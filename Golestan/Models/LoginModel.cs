using System.ComponentModel.DataAnnotations;

namespace Golestan.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "کد دانشجویی را وارد کنید")]
        public int Id { get; set; }

        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        public string Password { get; set; }

        [Required(ErrorMessage = "نقش را انتخاب کنید")]
        public string SelectedRole { get; set; }
    }
}
