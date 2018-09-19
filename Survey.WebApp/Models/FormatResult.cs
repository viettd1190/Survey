using System.Collections.Generic;

namespace Survey.WebApp.Models
{
    public class FormatResult<T>
        where T : class
    {
        public FormatResult()
        {
            rows = new List<T>();
        }

        public FormatResult(int total,
                            IEnumerable<T> query) : this()
        {
            this.total = total;
            rows = query;
        }

        /// <summary>
        ///     Tổng số bản ghi
        /// </summary>
        public int total { get; set; }

        /// <summary>
        ///     Data
        /// </summary>
        public IEnumerable<T> rows { get; set; }
    }
}
