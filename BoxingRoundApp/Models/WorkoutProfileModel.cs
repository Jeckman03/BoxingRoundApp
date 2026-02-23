using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BoxingRoundApp.Models
{
    public class WorkoutProfileModel : BaseModel
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
