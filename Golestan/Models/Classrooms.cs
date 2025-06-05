
﻿using System.ComponentModel.DataAnnotations;

namespace Golestan.Models
{
    public class Classrooms
    {
        [Key]

        public int Id { get; set; }
        public string Building {  get; set; }
        public int Room_Number {  get; set; }
        public int Capacity {  get; set; }
        public List<Sections> sections { get; set; }
    }
}//     Class Not Matched
