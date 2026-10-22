namespace atMISEDU.Contract.Constants
{
    /// <summary>
    /// Contain all constant for application
    /// </summary>
    public static class Const
    {
        #region Connection

        public const string CONN_CONFIG_SQL = "DbSqlServer";

        #endregion

        #region FileUpload

        public const string UPLOADED_SETTINGS = "UploadedSettings";
        public const string DOMAIN_HOSTS = "DomainHosts";
        public const string OUTPUTFILE_UPLOADED = "assets/images/uploaded";
        public const string FILENAME_CONFIG_ICON = "img-icon-{0}";
       
        #endregion

        #region Time zone

        /// <summary>
        /// UTC +7 = 3600 x (+7)
        /// </summary>
        public static int TIME_ZONE_UTC_7 = 25200;

        /// <summary>
        /// Seconds In A Day
        /// </summary>
        public const int SECONDS_IN_ADAY = 86400;

        #endregion
    }
}