using System;
using System.ComponentModel.DataAnnotations.Schema;
using Survey.Model.Abstract;

namespace Survey.Model.Models
{
    /// <summary>
    ///     Bảng chứa kết quả làm bài khảo sát
    /// </summary>
    [Table("Results")]
    public class Result : Auditable
    {
        /// <summary>
        ///     User tham gia khảo sát
        /// </summary>
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        /// <summary>
        ///     Id bài khảo sát
        /// </summary>
        public int SurveyId { get; set; }

        [ForeignKey("SurveyId")]
        public virtual Survey Survey { get; set; }

        /// <summary>
        ///     Thời gian bắt đầu thực hiện
        /// </summary>
        public DateTime FromTime { get; set; }

        /// <summary>
        ///     Thời gian hoàn thành
        /// </summary>
        public DateTime? ToTime { get; set; }

        /// <summary>
        ///     Số câu trả lời đúng
        /// </summary>
        public int CorrectAnswer { get; set; }
    }
}
