using System.ComponentModel.DataAnnotations;

namespace Golestan.Models
{
    public class Students
    {
        [Key]
        public int Student_Id { get; set; }
        public int User_Id { get; set; }
        public DateTime Enrollment_Date { get; set; }
        public Users users { get; set; }
    }
}
