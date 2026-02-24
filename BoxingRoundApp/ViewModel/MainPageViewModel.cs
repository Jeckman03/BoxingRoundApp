using BoxingRoundApp.Models;
using BoxingRoundApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BoxingRoundApp.ViewModel
{
    public partial class MainPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<WorkoutProfileModel> _workoutProfiles = new();

        [ObservableProperty]
        private int _selecetedProfileId;

        public MainPageViewModel()
        {
            WorkoutProfiles.Add(new WorkoutProfileModel { Name = "Killer Workout", Rounds = 10, TotalTime = 23 });
            WorkoutProfiles.Add(new WorkoutProfileModel { Name = "Easy Workout", Rounds = 5, TotalTime = 10 });
            WorkoutProfiles.Add(new WorkoutProfileModel { Name = "Power Workout", Rounds = 6, TotalTime = 15 });
        }

        [RelayCommand]
        private async Task CreateNewProfile()
        {
            await Shell.Current.GoToAsync("CreateWorkoutProfilePage");
        }
    }
}
