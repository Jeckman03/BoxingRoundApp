using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingRoundApp.Models
{
    public class RoundSettingsModel : BaseModel
    {
        [Indexed]
        public int WorkoutProfileId { get; set; }
        public string? RoundDescription { get; set; }
        public int RoundNumber { get; set; }
        public int DurationSeconds { get; set; } = 0;
        public int RestSeconds { get; set; } = 0;
    }
}
