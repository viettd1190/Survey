using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Survey.Model.Abstract;

namespace Survey.Model.Models
{
    [Table("Questions")]
    public class Question : Auditable
    {
        /// <summary>
        ///     Khóa ngoại cho biết câu hỏi này thuộc bản khảo sát nào
        /// </summary>
        public int SurveyId { get; set; }

        [ForeignKey("SurveyId")]
        public virtual Survey Survey { get; set; }

        /// <summary>
        ///     Nội dung câu hỏi
        /// </summary>
        public string Content { get; set; }

        public virtual IEnumerable<Answer> Answers { get; set; }
    }
}
