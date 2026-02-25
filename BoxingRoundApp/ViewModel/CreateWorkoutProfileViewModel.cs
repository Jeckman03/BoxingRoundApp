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
    [QueryProperty(nameof(Source), "Source")]
    [QueryProperty(nameof(ProfileId), "ProfileId")]
    public partial class CreateWorkoutProfileViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<RoundSettingsModel> _rounds = new();

        [ObservableProperty]
        private string _workoutName;

        [ObservableProperty]
        private int profileId;

        [ObservableProperty]
        private WorkoutProfileModel _currentWorkout;

        [ObservableProperty]
        private string source;

        public int RoundCount { get; set; } = 0;

        private readonly BoxingDatabase _boxingDatabase;

        public CreateWorkoutProfileViewModel(BoxingDatabase boxingDatabase)
        {
            _boxingDatabase = boxingDatabase;
        }

        public async Task LoadProfileToEditAsync()
        {
            try
            {
                IsBusy = true;

                if (ProfileId == 0)
                {
                    return;
                }
                else
                {
                    CurrentWorkout = await _boxingDatabase.GetProfileByIdAsync(ProfileId);
                    Rounds = new ObservableCollection<RoundSettingsModel>(await _boxingDatabase.GetRoundSettingsAsync(ProfileId));

                    RoundCount = Rounds.Count;
                    WorkoutName = CurrentWorkout.Name;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error failed to load existing profile for edit: {ex.Message}");
            }
            finally { IsBusy = false; }
        }

        [RelayCommand]
        private async Task AddRound()
        {
            RoundCount++;

            if (Rounds.Count == 0)
            {
                Rounds.Add(new RoundSettingsModel { RoundNumber = RoundCount });
            }
            else
            {
                var lastRound = Rounds[Rounds.Count - 1];
                Rounds.Add(new RoundSettingsModel { RoundNumber = RoundCount, DurationSeconds = lastRound.DurationSeconds, RestSeconds = lastRound.RestSeconds });
            }
        }

        [RelayCommand]
        private async Task RemoveRound()
        {
            if (RoundCount <= 0)
            {
                return;
            }

            var removedRound = Rounds[Rounds.Count-1];
            // Remove round from UI
            Rounds.RemoveAt(Rounds.Count - 1);
            RoundCount--;
            await _boxingDatabase.DeleteRoundSettingsAsync(removedRound.Id);
        }

        [RelayCommand]
        private async Task SaveWorkoutProfile()
        {

            try
            {
                IsBusy = true;

                bool hasInvalidRounds = Rounds.Any(r => r.DurationSeconds <= 0);

                if (hasInvalidRounds)
                {
                    await Shell.Current.DisplayAlertAsync("Error", "All rounds have a duration greater than 0", "OK");
                    return;
                }

                if (ProfileId == 0)
                {
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
                }
                else
                {
                    CurrentWorkout.Name = WorkoutName;
                    CurrentWorkout.TotalTime = WorkoutProfileServices.CalculateTotalWorkoutTime(Rounds);
                    CurrentWorkout.Rounds = WorkoutProfileServices.CalculateTotalRounds(Rounds);

                    await _boxingDatabase.SaveProfileAsync(CurrentWorkout);


                    foreach (var round in Rounds)
                    {
                        round.WorkoutProfileId = CurrentWorkout.Id;
                        round.RoundDescription = round.RoundDescription;
                        round.RoundNumber = round.RoundNumber;
                        round.DurationSeconds = round.DurationSeconds;
                        round.RestSeconds = round.RestSeconds;

                        await _boxingDatabase.SaveRoundSettingsAsync(round);
                    }

                }
                
                if (Source == "ActivateWorkout")
                {
                    await Shell.Current.GoToAsync("../..");
                }
                else
                {
                    await Shell.Current.GoToAsync("..");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error with saving Profile and Rounds {ex.Message}");
            }
            finally { IsBusy = false; }
        }
    }
}
