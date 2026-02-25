using BoxingRoundApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BoxingRoundApp.Services.Workouts
{
    public static class WorkoutProfileServices
    {
        public static int CalculateTotalRounds(IEnumerable<RoundSettingsModel> rounds)
        {
            return rounds.Count();
        }

        public static string CalculateTotalWorkoutTime(IEnumerable<RoundSettingsModel> rounds)
        {
            int totalSeconds = 0;

            foreach (var round in rounds) 
            {
                totalSeconds += round.DurationSeconds;
                totalSeconds += round.RestSeconds;
            }

            TimeSpan t = TimeSpan.FromSeconds(totalSeconds);

            return $"{t.Hours:D1}:{t.Minutes:D2}:{t.Seconds:D2}";
        }
    }
}
