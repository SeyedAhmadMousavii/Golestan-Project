using System.Collections.Generic;

namespace Golestan.Models
{
    public class SectionDetailsViewModel
    {
        public List<Users> Users { get; set; }
        public Courses courses { get; set; }
        public Sections Section { get; set; }
        public List<Students> Students {  get; set; }
        public List<Takes> Takes { get; set; }
    }

    //public class StudentGradeModel
    //{
    //    public Students Student { get; set; }
    //    public float? Grade { get; set; }
    //}
}
