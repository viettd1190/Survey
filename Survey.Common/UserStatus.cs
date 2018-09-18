namespace Survey.Common
{
    public enum UserStatus
    {
        /// <summary>
        ///     Đang chờ duyệt
        /// </summary>
        PENDING = 1,

        /// <summary>
        ///     Bị từ chối
        /// </summary>
        DENIED = 2,

        /// <summary>
        ///     Đang hoạt động
        /// </summary>
        ACTIVE = 3,

        /// <summary>
        ///     Tạm khóa
        /// </summary>
        LOCKED = 4
    }
}
