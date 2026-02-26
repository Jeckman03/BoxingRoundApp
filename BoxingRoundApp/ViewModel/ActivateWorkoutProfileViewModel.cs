using BoxingRoundApp.Models;
using BoxingRoundApp.Services.Data;
using BoxingRoundApp.Services.Workouts;
using BoxingRoundApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
            await Shell.Current.GoToAsync($"{nameof(CreateWorkoutProfilePage)}?ProfileId={ProfileId}&Source=ActivateWorkout");
        }

        [RelayCommand]
        private async Task StartWorkout()
        {
            var navigationParameter = new Dictionary<string, object>
            {
                {"Rounds", Rounds.ToList() }
            };
            await Shell.Current.GoToAsync($"StartWorkoutPage", navigationParameter);
        }
    }
}
