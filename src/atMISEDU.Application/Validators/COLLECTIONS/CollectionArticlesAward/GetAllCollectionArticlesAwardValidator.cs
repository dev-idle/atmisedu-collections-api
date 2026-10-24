using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesAward;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesAward
{
    /// <summary>
    /// Validator for <see cref="GetAllCollectionArticlesAwardRequest"/>
    /// </summary>
    public class GetAllCollectionArticlesAwardValidator : Validator<GetAllCollectionArticlesAwardRequest>
    {
        public GetAllCollectionArticlesAwardValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_AWARD_INVALID);
            RuleFor(x => x.ArticleId).GreaterThan(0);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
