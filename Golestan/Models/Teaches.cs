
﻿using System.ComponentModel.DataAnnotations;

namespace Golestan.Models

{
    public class Teaches
    {
        public int Instructor_Id {  get; set; }

        public Instructors instructors { get; set; }
        public int Section_Id {  get; set; }
        public Sections sections { get; set; }

    }
}//     Class Not Matched
