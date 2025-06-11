
﻿using System.ComponentModel.DataAnnotations;

namespace Golestan.Models
{
    public class Courses
    {
        [Key]
        public int Id { get; set; }
        public int CoursId {  get; set; }
        public string Title { get; set; }
        public string Code {  get; set; }
        public string Unit {  get; set; }
        public string Description { get; set; }
        public DateTime Final_Exam_Date { get; set; }
        public int Department_Id {  get; set; }
        public Departments departments { get; set; }
        public int? section_id {  get; set; }
        public Sections? section { get; set; }
        public int Prerequisite { get; set; }

    }
}//     Class Not Matched
