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
    [QueryProperty(nameof(ProfileId), "ProfileId")]
    public partial class ActivateWorkoutProfileViewModel : BaseViewModel
    {
        private readonly BoxingDatabase _boxingDatabase;

        [ObservableProperty]
        private int profileId;

        [ObservableProperty]
        private WorkoutProfileModel _profile;

        [ObservableProperty]
        private ObservableCollection<RoundSettingsModel> _rounds;

        public ActivateWorkoutProfileViewModel(BoxingDatabase boxingDatabase)
        {
            _boxingDatabase = boxingDatabase;
        }

        partial void OnProfileIdChanged(int value)
        {
            _ = LoadWorkoutData(value);
        }

        private async Task LoadWorkoutData(int id)
        {
            try
            {
                IsBusy = true;
                Profile = await _boxingDatabase.GetProfileByIdAsync(id);
                Rounds = new ObservableCollection<RoundSettingsModel>(await _boxingDatabase.GetRoundSettingsAsync(id));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading workout: {ex.Message}");
            }
            finally { IsBusy = false; }
        }

        [RelayCommand]
        private async Task EditRounds()
        {

        }

        [RelayCommand]
        private async Task StartWorkout()
        {

        }
    }
}
