using Survey.Common;

namespace Survey.WebApp.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Origanization { get; set; }

        public string Specification { get; set; }

        public UserRole Role { get; set; }

        public UserStatus Status { get; set; }

        public string LastLoginDate { get; set; }
    }
}
