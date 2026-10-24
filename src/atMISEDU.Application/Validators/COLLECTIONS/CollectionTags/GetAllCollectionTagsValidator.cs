using atMISEDU.Application.Requests.COLLECTIONS.CollectionTags;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionTags
{
    /// <summary>
    /// Validator for <see cref="GetAllCollectionTagsRequest"/>
    /// </summary>
    public class GetAllCollectionTagsValidator : Validator<GetAllCollectionTagsRequest>
    {
        public GetAllCollectionTagsValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_TAGS_INVALID);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
