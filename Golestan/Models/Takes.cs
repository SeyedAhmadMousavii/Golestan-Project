using System.ComponentModel.DataAnnotations;

namespace Golestan.Models
{
    public class Takes
    {
        [Key]
        public int Student_Id {  get; set; }
        public Students students { get; set; }
        public int Section_Id {  get; set; }
        public Sections sections { get; set; }
        public int Grade {  get; set; }
    }
}//     Class Not Matched
