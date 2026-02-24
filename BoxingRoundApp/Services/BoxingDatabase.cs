using BoxingRoundApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingRoundApp.Services
{
    public class BoxingDatabase
    {
        SQLiteAsyncConnection _database;

        async Task Init()
        {
            if (_database != null)
                return;

            _database = new SQLiteAsyncConnection(DbSettings.DatabasePath, DbSettings.Flags);

            await _database.CreateTableAsync<WorkoutProfileModel>();
            await _database.CreateTableAsync<RoundSettingsModel>();
        }

        public async Task<int> SaveProfileAsync(WorkoutProfileModel profile)
        {
            await Init();
            if (profile.Id != 0)
            {
                return await _database.UpdateAsync(profile);
            }
            else
            {
                return await _database.InsertAsync(profile);
            }
        }

        public async Task<int> SaveRoundSettingsAsync(RoundSettingsModel roundSettings)
        {
            await Init();
            if (roundSettings.Id != 0)
            {
                return await _database.UpdateAsync(roundSettings);
            }
            else
            {
                return await _database.InsertAsync(roundSettings);
            }
        }

        public async Task<List<WorkoutProfileModel>> GetProfilesAsync()
        {
            await Init();
            return await _database.Table<WorkoutProfileModel>().ToListAsync();
        }

        public async Task<List<RoundSettingsModel>> GetRoundSettingsAsync(int profileId)
        {
            await Init();
            return await _database.Table<RoundSettingsModel>()
                .Where(rs => rs.WorkoutProfileId == profileId)
                .OrderBy(rs => rs.RoundNumber)
                .ToListAsync();
        }

        public async Task<WorkoutProfileModel> GetProfileByIdAsync(int id)
        {
            await Init();
            return await _database.Table<WorkoutProfileModel>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async void DeleteProfileAsync(int profileId)
        {
            await Init();
            var profile = await _database.Table<WorkoutProfileModel>().Where(p => p.Id == profileId).FirstOrDefaultAsync();
            if (profile != null)
            {
                await _database.DeleteAsync(profile);
                // Also delete associated round settings
                var roundSettings = await _database.Table<RoundSettingsModel>().Where(rs => rs.WorkoutProfileId == profileId).ToListAsync();
                foreach (var rs in roundSettings)
                {
                    await _database.DeleteAsync(rs);
                }
            }
        }

        public async void DeleteRoundSettingsAsync(int roundSettingsId)
        {
            await Init();
            var roundSettings = await _database.Table<RoundSettingsModel>().Where(rs => rs.Id == roundSettingsId).FirstOrDefaultAsync();
            if (roundSettings != null)
            {
                await _database.DeleteAsync(roundSettings);
            }
        }
    }
}
