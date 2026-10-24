using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesAward;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesAward
{
    /// <summary>
    /// Validator for <see cref="GetDetailCollectionArticlesAwardRequest"/>
    /// </summary>
    public class GetDetailCollectionArticlesAwardValidator : Validator<GetDetailCollectionArticlesAwardRequest>
    {
        public GetDetailCollectionArticlesAwardValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_AWARD_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
