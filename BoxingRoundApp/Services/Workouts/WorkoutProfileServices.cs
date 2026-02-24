using BoxingRoundApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BoxingRoundApp.Services.Workouts
{
    public static class WorkoutProfileServices
    {
        public static int CalculateTotalRounds(ObservableCollection<RoundSettingsModel> rounds)
        {
            return rounds.Count();
        }

        public static int CalculateTotalWorkoutTime(ObservableCollection<RoundSettingsModel> rounds)
        {
            int totalSeconds = 0;

            foreach (var round in rounds) 
            {
                totalSeconds += round.DurationSeconds;
                totalSeconds += round.RestSeconds;
            }

            TimeSpan totalTimeSec = TimeSpan.FromSeconds(totalSeconds);

            return totalSeconds;
        }
    }
}
