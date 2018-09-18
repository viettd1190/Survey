using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Survey.Model.Abstract;

namespace Survey.Model.Models
{
    [Table("Surveys")]
    public class Survey : Auditable
    {
        /// <summary>
        ///     Tiêu đề cuộc khảo sát
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Ngày bắt đầu
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        ///     Ngày kết thúc
        /// </summary>
        public DateTime ToDate { get; set; }

        /// <summary>
        ///     Người tạo
        /// </summary>
        public int CreatedByUser { get; set; }

        public virtual IEnumerable<Question> Questions { get; set; }
    }
}
