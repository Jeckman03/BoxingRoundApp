using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingRoundApp.Models
{
    public partial class RoundSettingsModel : BaseModel
    {
        [Indexed]
        public int WorkoutProfileId { get; set; }
        [ObservableProperty]
        private string? roundDescription;
        public int RoundNumber { get; set; }
        public int DurationSeconds { get; set; }
        public int RestSeconds { get; set; }
    }
}
