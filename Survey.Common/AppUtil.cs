namespace Survey.Common
{
    public class AppUtil
    {
        /// <summary>
        ///     format string kiểu ngày tháng năm
        /// </summary>
        public const string DATE_PARAM_FORMAT = "{0:dd-MM-yyyy}";

        /// <summary>
        ///     format string kiểu ngày tháng năm giờ phút giây
        /// </summary>
        public const string DATE_TIME_PARAM_FORMAT = "{0:dd-MM-yyyy HH:mm:ss}";

        /// <summary>
        ///     format string kiểu số
        /// </summary>
        public static string MONEY_FORMAT = "{0:#,##0}";
    }
}
