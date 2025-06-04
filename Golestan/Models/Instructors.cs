namespace Golestan.Models
{
    public class Instructors
    {
        public int Instructor_Id { get; set; }
        public int User_Id { get; set; }
        public decimal Salary {  get; set; }
        public DateTime Hire_Date { get; set; }
        public int Department_Id {  get; set; }
        public List<Teaches> teaches { get; set; }
    }
}//     Class Not Matched
