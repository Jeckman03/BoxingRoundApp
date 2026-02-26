
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
    [QueryProperty(nameof(Rounds), "Rounds")]
    public partial class StartWorkoutViewModel : BaseViewModel
    {
        public TimerServices _timerServices;
        public AudioManager AudioManager { get; }
        [ObservableProperty]
        private List<RoundSettingsModel> _rounds;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FormattedTime))]
        private int displayTime;

        [ObservableProperty]
        private string currentPahse;

        [ObservableProperty]
        private bool isWorkPhase;

        [ObservableProperty]
        private string currentCombo;

        public Color TimerColor => (IsWorkPhase && DisplayTime <= 10) ? Colors.OrangeRed : Colors.White;

        public string FormattedTime
        { 
            get
            {
                int minutes = DisplayTime / 60;
                int seconds = DisplayTime % 60;

                return $"{minutes}:{seconds:D2}";
            }
        }

        public StartWorkoutViewModel(TimerServices timerServices, AudioManager audioManager)
        {
            _timerServices = timerServices;
            AudioManager = audioManager;
        }

        [RelayCommand]
        public async Task StartWorkoutAsync()
        {
            if (Rounds == null || Rounds.Count == 0) return;

            await _timerServices.RunWorkout(Rounds, 
                                            (time) => DisplayTime = time,
                                            (phase) => 
                                            {
                                                CurrentPahse = phase;
                                                IsWorkPhase = !phase.Contains("Rest");
                                            },
                                            () => { });
        }
    }
}
