using atMISEDU.Application.Requests.COLLECTIONS.CollectionTags;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;
using atMISEDU.Domain.Constants.COLLECTIONS;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionTags
{
    /// <summary>
    /// Validator for <see cref="UpdateCollectionTagsRequest"/>
    /// </summary>
    public class UpdateCollectionTagsValidator : Validator<UpdateCollectionTagsRequest>
    {
        /// <summary>
        /// Constructor of <see cref="UpdateCollectionTagsValidator"/>, register validator rules for <see cref="UpdateCollectionTagsRequest"/>
        /// </summary>
        public UpdateCollectionTagsValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_TAGS_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.TagsName).NotEmpty().MaxLength(CollectionTagsConst.TAGS_NAME_MAX_LENGTH);
            RuleFor(x => x.TagsDescription).MaxLength(CollectionTagsConst.TAGS_DESCRIPTION_MAX_LENGTH);
            RuleFor(x => x.TagsIcon).MaxLength(CollectionTagsConst.TAGS_ICON_MAX_LENGTH);
            RuleFor(x => x.TagsStyle).MaxLength(CollectionTagsConst.TAGS_STYLE_MAX_LENGTH);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
