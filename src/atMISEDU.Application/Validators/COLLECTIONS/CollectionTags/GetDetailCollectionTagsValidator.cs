using atMISEDU.Application.Requests.COLLECTIONS.CollectionTags;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionTags
{
    /// <summary>
    /// Validator for <see cref="GetDetailCollectionTagsRequest"/>
    /// </summary>
    public class GetDetailCollectionTagsValidator : Validator<GetDetailCollectionTagsRequest>
    {
        public GetDetailCollectionTagsValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_TAGS_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
