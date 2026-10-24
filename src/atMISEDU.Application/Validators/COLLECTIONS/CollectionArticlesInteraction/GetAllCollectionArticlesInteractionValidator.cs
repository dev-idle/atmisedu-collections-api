using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesInteraction;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesInteraction
{
    /// <summary>
    /// Validator for <see cref="GetAllCollectionArticlesInteractionRequest"/>
    /// </summary>
    public class GetAllCollectionArticlesInteractionValidator : Validator<GetAllCollectionArticlesInteractionRequest>
    {
        public GetAllCollectionArticlesInteractionValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_INTERACTION_INVALID);
            RuleFor(x => x.ArticleId).GreaterThan(0);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
