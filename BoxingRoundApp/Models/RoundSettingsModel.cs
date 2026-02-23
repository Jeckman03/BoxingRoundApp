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
        public int RoundNumber { get; set; }
        public int DurationSeconds { get; set; }
        public int RestSeconds { get; set; }
    }
}
