using System.Collections.Generic;

namespace Survey.Model.Abstract
{
    public class UpdateResult : IUpdateResult
    {
        public static string UPDATE_SUCCESS = "Cập nhật thành công";

        public static string ERROR_WHEN_UPDATED = "Lỗi không xác định";

        public static string INFORMATION_HAS_EXISTS = "informationhasexists";

        public static string USER_NOT_FOUND = "Thông tin người dùng không tồn tại";

        public static string USER_NOT_ENOUGH_PERMISSON = "usernotenoughtpermission";

        public static string PASSWORD_NOT_MATCH = "passwordnotmatch";

        public UpdateResult()
        {
            State = 1;
            KeyLanguage = "updatesuccessful";
            KeyLanguages = new List<string>();
        }

        public UpdateResult(object obj) : this()
        {
            Value = obj;
        }

        #region IUpdateResult Members

        /// <summary>
        ///     1 : Success
        ///     2 : Information
        ///     3 : Warning
        ///     4 : Error
        /// </summary>
        public int State { get; set; }

        /// <summary>
        ///     lỗi
        /// </summary>
        public string KeyLanguage { get; set; }

        /// <summary>
        ///     danh sách lỗi
        /// </summary>
        public List<string> KeyLanguages { get; set; }

        public object Value { get; set; }

        #endregion
    }
}
