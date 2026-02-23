using BoxingRoundApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BoxingRoundApp.ViewModel
{
    public partial class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<WorkoutProfileModel> WorkoutProfiles = new();

        public MainPageViewModel()
        {
            WorkoutProfiles.Add(new WorkoutProfileModel { Name = "Killer Workout" });
            WorkoutProfiles.Add(new WorkoutProfileModel { Name = "Easy Workout" });
            WorkoutProfiles.Add(new WorkoutProfileModel { Name = "Power Workout" });
        }
    }
}
