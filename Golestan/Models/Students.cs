using System.ComponentModel.DataAnnotations;

namespace Golestan.Models
{
    public class Students
    {
        [Key]
        public int Id {  get; set; }
        public int Student_Id { get; set; }
        public int User_Id { get; set; }
        public Users User { get; set; }
        public DateTime Enrollment_Date { get; set; }
        public int Depatment_Id { get; set; }
        public Departments departments { get; set; }
        public List<Takes>takes { get; set; }
    }
}//     Class Not Matched
