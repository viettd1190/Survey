using System.ComponentModel.DataAnnotations.Schema;
using Survey.Common;
using Survey.Model.Abstract;

namespace Survey.Model.Models
{
    [Table("Logs")]
    public class Log : Auditable
    {
        /// <summary>
        ///     Tiêu đề
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Nội dung
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Trạng thái
        /// </summary>
        public LogStatus Status { get; set; }
    }
}
