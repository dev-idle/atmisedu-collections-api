namespace atMISEDU.Domain.Constants.COLLECTIONS
{
    public class CollectionTagsConst
    {
        #region Database defines

        public const string FIELD_ID = "keyTags";
        public const string FIELD_TAGS_NAME = "tagsName";
        public const string FIELD_TAGS_DESCRIPTION = "tagsDescription";
        public const string FIELD_TAGS_ICON = "tagsIcon";
        public const string FIELD_TAGS_STYLE = "tagsStyle";
        public const string FIELD_CREATED_BY = "createdBy";
        public const string FIELD_CREATED_DATE = "createdDate";
        public const string FIELD_IS_ACTIVED = "isActived";

        #endregion

        #region Max length defines

        public const int TAGS_NAME_MAX_LENGTH = 64;
        public const int TAGS_DESCRIPTION_MAX_LENGTH = 512;
        public const int TAGS_ICON_MAX_LENGTH = 128;
        public const int TAGS_STYLE_MAX_LENGTH = 128;

        #endregion
    }
}