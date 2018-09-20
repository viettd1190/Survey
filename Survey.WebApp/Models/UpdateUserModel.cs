using System.ComponentModel.DataAnnotations;
using Survey.Common;

namespace Survey.WebApp.Models
{
    public class UpdateUserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã số người dùng không được để trống")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không chính xác")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Tên người dùng không được để trống")]
        public string Name { get; set; }

        public string Origanization { get; set; }

        public string Specification { get; set; }

        public string FromDate { get; set; }

        public UserRole Role { get; set; }

        public UserStatus Status { get; set; }

        public string LastLoginDate { get; set; }
    }
}
