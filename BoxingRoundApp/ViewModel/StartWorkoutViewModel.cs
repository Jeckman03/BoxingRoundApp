using BoxingRoundApp.Models;
using BoxingRoundApp.Services.Data;
using BoxingRoundApp.Services.Workouts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BoxingRoundApp.ViewModel
{
    [QueryProperty(nameof(ProfileId), "ProfileId")]
    public partial class StartWorkoutViewModel : BaseViewModel
    {
        private readonly BoxingDatabase _boxingDatabase;
        public TimerServices _timerServices;

        [ObservableProperty]
        private ObservableCollection<RoundSettingsModel> _rounds = new();

        [ObservableProperty]
        private int profileId;

        [ObservableProperty]
        private int displayTime;

        [ObservableProperty]
        private string currentPahse;

        public StartWorkoutViewModel(BoxingDatabase boxingDatabase, TimerServices timerServices)
        {
            _boxingDatabase = boxingDatabase;
            _timerServices = timerServices;
        }

        [RelayCommand]
        private async Task StartWorkout(List<RoundSettingsModel> rounds)
        {
            await _timerServices.RunWorkout(rounds, (time) => DisplayTime = time, (phase) => CurrentPahse = phase);
        }
    }
}
