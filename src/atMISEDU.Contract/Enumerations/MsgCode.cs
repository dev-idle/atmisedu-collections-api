using System.Text.Json.Serialization;

namespace atMISEDU.Contract.Enumerations
{
    /// <summary>
    /// Enum to define error code, use for decompile into message for end user
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MsgCode
    {
        #region FileSERVER msg codes

        /// <summary>
        /// File size exceed limit
        /// </summary>
        ERR_FILE_TOO_LARGE,

        /// <summary>
        /// File extension is not permitted
        /// </summary>
        ERR_INVALID_FILE,

        #endregion

        ERR_UNSUPPORTED_MEDIA_TYPE,

        ERR_BAD_REQUEST,

        ERR_UNAUTHORIZED,

        ERR_FORBIDDENT,

        ERR_USER_LOGIN_FAIL,

        ERR_FILE_PROCESSING_FAILED,

        #region Base msg codes

        /// <summary>
        /// Define error code for invalid email format
        /// </summary>
        /// <remarks>
        /// Correct example: abc@gmail.com
        /// </remarks>
        ERR_INVALID_EMAIL,

        /// <summary>
        /// Define error code for invalid phone format
        /// </summary>
        /// <remarks>
        /// Correct example: +84 123 123 1234
        /// </remarks>
        ERR_INVALID_PHONE,

        /// <summary>
        /// Define error for invalid key format
        /// </summary>
        /// <remarks>
        /// Correct example: ABC_123
        /// </remarks>
        ERR_INVALID_KEY,

        /// <summary>
        /// Define error code for internal server error
        /// </summary>
        ERR_INTERNAL_SERVER,

        /// <summary>
        /// Define error code for not found resources
        /// </summary>
        ERR_NOT_FOUND,

        /// <summary>
        /// Define error code for conflict between resources
        /// </summary>
        ERR_CONFLICT,

        /// <summary>
        /// Define error code for unexpected validation exception
        /// </summary>
        ERR_INVALID,

        /// <summary>
        /// Define error code for not found resources find by key
        /// </summary>
        ERR_NF_FIND_KEY,

        #endregion

        #region Utils

        ERR_DATE_NOT_SUPPORTED,

        #endregion

        #region CatalogUnit msg codes

        /// <summary>
        /// CatalogUnit request is invalid
        /// </summary>
        ERR_CATALOG_CATALOG_UNIT_INVALID,

        /// <summary>
        /// CatalogUnit with the id provided was not found
        /// </summary>
        ERR_CATALOG_CATALOG_UNIT_ID_NOT_FOUND,

        #endregion

        #region HubsChannel msg codes

        /// <summary>
        /// HubsChannel request is invalid
        /// </summary>
        ERR_HUBS_HUBS_CHANNEL_INVALID,

        /// <summary>
        /// HubsChannel with the id provided was not found
        /// </summary>
        ERR_HUBS_HUBS_CHANNEL_ID_NOT_FOUND,

        #endregion
    }
}
