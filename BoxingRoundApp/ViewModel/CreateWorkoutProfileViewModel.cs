using BoxingRoundApp.Models;
using BoxingRoundApp.Services.Data;
using BoxingRoundApp.Services.Workouts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace BoxingRoundApp.ViewModel
{
    public partial class CreateWorkoutProfileViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<RoundSettingsModel> _rounds = new();

        [ObservableProperty]
        private string _workoutName;

        [ObservableProperty]
        private int _roundTime;

        [ObservableProperty]
        private int _roundRestTime;

        [ObservableProperty]
        private string _roundDescription;

        public int RoundCount { get; set; } = 0;

        private readonly BoxingDatabase _boxingDatabase;

        public CreateWorkoutProfileViewModel(BoxingDatabase boxingDatabase)
        {
            _boxingDatabase = boxingDatabase;
        }

        [RelayCommand]
        private async Task AddRound()
        {
            RoundCount++;
            // Add round field in UI
            Rounds.Add(new RoundSettingsModel { RoundNumber = RoundCount});
        }

        [RelayCommand]
        private async Task RemoveRound()
        {
            if (RoundCount <= 0)
            {
                return;
            }
            // Remove round from UI
            Rounds.RemoveAt(Rounds.Count - 1);
            RoundCount--;
        }

        [RelayCommand]
        private async Task SaveWorkoutProfile()
        {

            try
            {
                IsBusy = true;

                WorkoutProfileModel newProfile = new WorkoutProfileModel();
                newProfile.Name = WorkoutName;
                newProfile.TotalTime = WorkoutProfileServices.CalculateTotalWorkoutTime(Rounds);
                newProfile.Rounds = WorkoutProfileServices.CalculateTotalRounds(Rounds);

                await _boxingDatabase.SaveProfileAsync(newProfile);


                foreach (var round in Rounds)
                {
                    RoundSettingsModel newRound = new();
                    newRound.WorkoutProfileId = newProfile.Id;
                    newRound.RoundDescription = round.RoundDescription;
                    newRound.RoundNumber = round.RoundNumber;
                    newRound.DurationSeconds = round.DurationSeconds;
                    newRound.RestSeconds = round.RestSeconds;

                    await _boxingDatabase.SaveRoundSettingsAsync(newRound);
                }

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error with saving Profile and Rounds {ex.Message}");
            }
            finally { IsBusy = false; }

        }
    }
}
