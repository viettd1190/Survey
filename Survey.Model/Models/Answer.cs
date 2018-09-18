using System.ComponentModel.DataAnnotations.Schema;
using Survey.Model.Abstract;

namespace Survey.Model.Models
{
    [Table("Answers")]
    public class Answer : Auditable
    {
        /// <summary>
        ///     Khóa ngoại cho biết câu trả lời thuộc câu hỏi nào
        /// </summary>
        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }

        /// <summary>
        ///     Nội dung câu trả lời
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     Câu trả lời đúng hay không
        /// </summary>
        public bool IsCorrect { get; set; }
    }
}
