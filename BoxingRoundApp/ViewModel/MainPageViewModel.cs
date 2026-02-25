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
using System.Text;

namespace BoxingRoundApp.ViewModel
{
    public partial class MainPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<WorkoutProfileModel> _workoutProfiles = new();

        [ObservableProperty]
        private WorkoutProfileModel _selectedProfile;

        private readonly BoxingDatabase _boxingDatabase;


        public MainPageViewModel(BoxingDatabase boxingDatabase)
        {
            _boxingDatabase = boxingDatabase;
        }

        public async Task LoadWorkoutProfilesAsync()
        {
            WorkoutProfiles.Clear();
            var profiles = await _boxingDatabase.GetProfilesAsync();
            var totalTime = 0;

            foreach (var profile in profiles)
            {
                WorkoutProfiles.Add(profile);

                var rounds = await _boxingDatabase.GetRoundSettingsAsync(profile.Id);
            }

        }

        [RelayCommand]
        private async Task CreateNewProfile()
        {
            await Shell.Current.GoToAsync("CreateWorkoutProfilePage");
        }

        [RelayCommand]
        private async Task ProfileSelection(WorkoutProfileModel SelectedProfile)
        {
            try
            {
                IsBusy = true;

                if (SelectedProfile == null)
                    return;

                await Shell.Current.GoToAsync($"{nameof(ActivateWorkoutProfilePage)}?ProfileId={SelectedProfile.Id}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally { IsBusy = false; }
        }

        [RelayCommand]
        private async Task ShowOptions(WorkoutProfileModel profile)
        {
            try
            {
                IsBusy = true;

                if (profile == null)
                {
                    Debug.WriteLine("Profile is null");
                    return;
                }

                int profileId = profile.Id;

                var result = await Shell.Current.DisplayActionSheetAsync("Worlout Profile Options", "Cancel", null, "Edit", "Delete");

                if (result == "Edit")
                {
                    // Go to the CreateWorkoutPage or create a new EditWorkoutPage
                }
                else if (result == "Delete")
                {
                    bool confirm = await Shell.Current.DisplayAlertAsync("Delete Profile", $"Are you sure you want to delete {profile.Name}?", "Yes", "No");

                    if (confirm)
                    {
                        await _boxingDatabase.DeleteProfileAsync(profile.Id);
                        WorkoutProfiles.Remove(profile); 
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error failed to Show edit and delete window {ex.Message}");
                throw;
            }
            finally { IsBusy = false; }
        }
    }
}
