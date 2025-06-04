namespace Golestan.Models
{
    public class Students
    {
        public int Student_Id {  get; set; }
        public int User_Id { get; set; }
        public DateTime Enrollment_Date { get; set; }
        public int Depatment_Id { get; set; }
        public List<Takes>takes { get; set; }
    }
}//     Class Not Matched
