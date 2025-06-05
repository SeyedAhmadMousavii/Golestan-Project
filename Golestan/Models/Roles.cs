using System.ComponentModel.DataAnnotations;

namespace Golestan.Models
{
    public enum role
    {
        Student = 1,
        Teacher = 2,
        Admin = 3
    }
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        public role Name { get; set; }
        public List<User_Role> User_Roles { get; set; }
    }
}
