using System.ComponentModel.DataAnnotations;

namespace Golestan.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created_at { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Email { get; set; }
        public string Hashed_password { get; set; }
        public List<User_Role> User_Roles { get; set; }
        public ICollection<Student> students { get; set; }

    }
}
