using BoxingRoundApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BoxingRoundApp.ViewModel
{
    public partial class CreateWorkoutProfileViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<RoundSettingsModel> _rounds = new();

        [ObservableProperty]
        private int _roundCount;

        public CreateWorkoutProfileViewModel()
        {
            Rounds.Add(new RoundSettingsModel { RoundNumber = 1, DurationSeconds = 60, RestSeconds = 30 });
            Rounds.Add(new RoundSettingsModel { RoundNumber = 2, DurationSeconds = 60, RestSeconds = 30 });
            Rounds.Add(new RoundSettingsModel { RoundNumber = 3, DurationSeconds = 60, RestSeconds = 30 });
            Rounds.Add(new RoundSettingsModel { RoundNumber = 4, DurationSeconds = 60, RestSeconds = 30 });
        }
    }
}
