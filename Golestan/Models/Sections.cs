using System.ComponentModel.DataAnnotations;

namespace Golestan.Models
{
    public class Sections
    {
        [Key]
        public int Id { get; set; }

        public int SectionId { get; set; }

        public int Course_Id { get; set; }
        public Courses courses { get; set; }

        public int Semester { get; set; }
        public int year { get; set; }

        public int Classroom_Id { get; set; }
        public Classrooms classrooms { get; set; }

        public int Time_Slot_Id { get; set; }
        public Time_Slots time_slots { get; set; }

        public Teaches teaches { get; set; }
        public Takes takes { get; set; }
    }
}
