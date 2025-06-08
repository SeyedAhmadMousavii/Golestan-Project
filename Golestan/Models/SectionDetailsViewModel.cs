using System.Collections.Generic;

namespace Golestan.Models
{
    public class SectionDetailsViewModel
    {
        public Sections Section { get; set; }
        public List<StudentGradeModel> Students { get; set; }
    }

    public class StudentGradeModel
    {
        public Students Student { get; set; }
        public float? Grade { get; set; }
    }
}
