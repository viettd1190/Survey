using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Survey.Model.Abstract;

namespace Survey.Model.Models
{
    [Table("SupportInformations")]
    public class SupportInformation : Auditable
    {
        /// <summary>
        ///     Email hỗ trợ
        /// </summary>
        [MaxLength(128)]
        [Column(TypeName = "varchar")]
        public string Email { get; set; }

        /// <summary>
        ///     Số điện thoại hỗ trợ
        /// </summary>
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Địa chỉ liên hệ
        /// </summary>
        public string Address { get; set; }
    }
}
