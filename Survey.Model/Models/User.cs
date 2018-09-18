using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Survey.Common;
using Survey.Model.Abstract;

namespace Survey.Model.Models
{
    [Table("Users")]
    public class User : Auditable
    {
        /// <summary>
        ///     Tên đăng nhập
        ///     - Nếu là admin thì tên đăng nhập mặc định là admin
        ///     - Nếu là sinh viên thì tên đăng nhập là mã sinh viên
        ///     - Nếu là nhân viên thì tên đăng nhập là mã nhân viên
        /// </summary>
        [MaxLength(128)]
        [Column(TypeName = "varchar")]
        public string Username { get; set; }

        /// <summary>
        ///     Mật khẩu được mã hóa
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Chuỗi mã hóa mật khẩu
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        ///     Tên đầy đủ
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     - Nếu là sinh viên: tên lớp
        ///     - Nếu là nhân viên: tên khoa
        /// </summary>
        [MaxLength(128)]
        public string Origanization { get; set; }

        /// <summary>
        ///     Đặc điểm
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        ///     Phân quyền
        ///     - 1: admin
        ///     - 2: nhân viên
        ///     - 3: sinh viên
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        ///     Trạng thái user
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        ///     - Nếu là sinh viên: Ngày nhập học
        ///     - Nếu là nhân viên: Ngày tham gia
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        ///     Đăng nhập lần cuối
        /// </summary>
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        ///     Đổi mật khẩu lần cuối
        /// </summary>
        public DateTime? LastChangePasswordDate { get; set; }
    }
}
