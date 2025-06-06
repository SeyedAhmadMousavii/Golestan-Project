
﻿using System.ComponentModel.DataAnnotations;

namespace Golestan.Models
{
    public class Instructors
    {
        [Key]
        public int Id { get; set; }
        public int Instructor_Id { get; set; }  
        public int User_Id { get; set; }
        public Users User { get; set; }

        public decimal Salary {  get; set; }
        public DateTime Hire_Date { get; set; }

        public int Department_Id {  get; set; }
        public Departments departments { get; set; }

        public List<Teaches> teaches { get; set; }
    }
}//     Class Not Matched
