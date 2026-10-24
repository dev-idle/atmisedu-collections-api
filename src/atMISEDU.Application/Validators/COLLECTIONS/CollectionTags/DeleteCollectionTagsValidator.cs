using atMISEDU.Application.Requests.COLLECTIONS.CollectionTags;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionTags
{
    /// <summary>
    /// Validator for <see cref="DeleteCollectionTagsRequest"/>
    /// </summary>
    public class DeleteCollectionTagsValidator : Validator<DeleteCollectionTagsRequest>
    {
        /// <summary>
        /// Constructor of <see cref="DeleteCollectionTagsValidator"/>, register validator rules for <see cref="DeleteCollectionTagsRequest"/>
        /// </summary>
        public DeleteCollectionTagsValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_TAGS_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
