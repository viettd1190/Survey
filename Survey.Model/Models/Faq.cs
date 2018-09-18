using System.ComponentModel.DataAnnotations.Schema;
using Survey.Model.Abstract;

namespace Survey.Model.Models
{
    [Table("Faqs")]
    public class Faq : Auditable
    {
        /// <summary>
        ///     Tiêu đề câu hỏi thường gặp
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Nội dung câu hỏi thường gặp
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     Hiển thị trang chủ hay không
        /// </summary>
        public bool IsDisplay { get; set; }
    }
}
