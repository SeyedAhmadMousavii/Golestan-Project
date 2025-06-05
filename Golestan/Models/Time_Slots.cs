
﻿using System.ComponentModel.DataAnnotations;

namespace Golestan.Models
{
    public class Time_Slots
    {
        [Key]
        public int Id {  get; set; }
        public string Day {  get; set; }
        public DateTime Start_Time { get; set; }
        public DateTime End_Time { get; set; }
        public List<Sections> sections { get; set; }
    }
}//     Class Not Matched
