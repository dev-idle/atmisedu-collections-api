namespace atMISEDU.Domain.Constants.COLLECTIONS
{
    public class CollectionArticlesDiscussionConst
    {
        #region Database defines

        public const string FIELD_ID = "discussionId";
        public const string FIELD_DISCUSSION_PID = "discussionPid";
        public const string FIELD_ARTICLE_ID = "articleId";
        public const string FIELD_CREATED_BY = "createdBy";
        public const string FIELD_DIS_CONTENT = "disContent";
        public const string FIELD_CREATED_DATE = "createdDate";
        public const string FIELD_IS_ACTIVED = "isActived";

        #endregion

        #region Max length defines

        public const int DIS_CONTENT_MAX_LENGTH = 4000;

        #endregion
    }
}