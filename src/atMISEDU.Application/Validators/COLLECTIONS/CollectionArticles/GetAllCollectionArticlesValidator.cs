using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticles;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticles
{
    /// <summary>
    /// Validator for <see cref="GetAllCollectionArticlesRequest"/>
    /// </summary>
    public class GetAllCollectionArticlesValidator : Validator<GetAllCollectionArticlesRequest>
    {
        public GetAllCollectionArticlesValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_INVALID);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
            RuleFor(x => x.KeyTags).GreaterThan(0);
        }
    }
}
