namespace atMISEDU.Domain.Constants.COLLECTIONS
{
    public class CollectionArticlesConst
    {
        #region Database defines

        public const string FIELD_ID = "articleId";
        public const string FIELD_ARTICLE_TITLE = "articleTitle";
        public const string FIELD_ARTICLE_ABSTRACT = "articleAbstract";
        public const string FIELD_ARTICLE_CONTENT = "articleContent";
        public const string FIELD_ARTICLE_THUMBNAIL = "articleThumbnail";
        public const string FIELD_ARTICLE_PUBLISHED_DATE = "articlePublishedDate";
        public const string FIELD_ARTICLE_TIME_READING = "articleTimeReading";
        public const string FIELD_ARTICLE_VIEWS = "articleViews";
        public const string FIELD_ARTICLE_IP_ADDRESS = "articleIPAddress";
        public const string FIELD_ARTICLE_BROWSER = "articleBrowser";
        public const string FIELD_ARTICLE_OSTYPE = "articleOstype";
        public const string FIELD_CREATED_BY = "createdBy";
        public const string FIELD_CREATED_DATE = "createdDate";
        public const string FIELD_IS_ACTIVED = "isActived";

        #endregion

        #region Max length defines

        public const int ARTICLE_TITLE_MAX_LENGTH = 256;
        public const int ARTICLE_ABSTRACT_MAX_LENGTH = 500;
        public const int ARTICLE_THUMBNAIL_MAX_LENGTH = 128;
        public const int ARTICLE_IP_ADDRESS_MAX_LENGTH = 32;
        public const int ARTICLE_BROWSER_MAX_LENGTH = 32;
        public const int ARTICLE_OSTYPE_MAX_LENGTH = 64;

        #endregion
    }
}