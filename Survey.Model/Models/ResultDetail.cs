using System.ComponentModel.DataAnnotations.Schema;
using Survey.Model.Abstract;

namespace Survey.Model.Models
{
    /// <summary>
    ///     Bảng kết quả chi tiết
    /// </summary>
    [Table("ResultDetails")]
    public class ResultDetail : Auditable
    {
        /// <summary>
        ///     Id user tham gia khảo sát
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     Id câu hỏi
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        ///     Id câu trả lời của user
        /// </summary>
        public int AnswerId { get; set; }
    }
}
