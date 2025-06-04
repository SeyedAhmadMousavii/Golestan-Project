namespace Golestan.Models
{
    public class Departments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Building { get; set; }
        public decimal Budget { get; set; }
        public List<Instructors> instructors { get; set; }
        public List<Students>students { get; set; }
        public List<Courses> courses { get; set; }
    }
}//     Class Not Matched
