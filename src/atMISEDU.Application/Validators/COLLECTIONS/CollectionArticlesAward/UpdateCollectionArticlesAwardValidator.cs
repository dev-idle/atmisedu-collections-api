using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesAward;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesAward
{
    /// <summary>
    /// Validator for <see cref="UpdateCollectionArticlesAwardRequest"/>
    /// </summary>
    public class UpdateCollectionArticlesAwardValidator : Validator<UpdateCollectionArticlesAwardRequest>
    {
        /// <summary>
        /// Constructor of <see cref="UpdateCollectionArticlesAwardValidator"/>, register validator rules for <see cref="UpdateCollectionArticlesAwardRequest"/>
        /// </summary>
        public UpdateCollectionArticlesAwardValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_AWARD_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.AwardId).GreaterThan(0);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
        }
    }
}
