using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticles;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Validators;
using atMISEDU.Domain.Constants.COLLECTIONS;

namespace atMISEDU.Application.Validators.COLLECTIONS.CollectionArticles
{
    /// <summary>
    /// Validator for <see cref="UpdateCollectionArticlesRequest"/>
    /// </summary>
    public class UpdateCollectionArticlesValidator : Validator<UpdateCollectionArticlesRequest>
    {
        /// <summary>
        /// Constructor of <see cref="UpdateCollectionArticlesValidator"/>, register validator rules for <see cref="UpdateCollectionArticlesRequest"/>
        /// </summary>
        public UpdateCollectionArticlesValidator()
        {
            WithValidator(MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_INVALID);
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.ArticleTitle).NotEmpty().MaxLength(CollectionArticlesConst.ARTICLE_TITLE_MAX_LENGTH);
            RuleFor(x => x.ArticleAbstract).MaxLength(CollectionArticlesConst.ARTICLE_ABSTRACT_MAX_LENGTH);
            RuleFor(x => x.ArticleThumbnail).MaxLength(CollectionArticlesConst.ARTICLE_THUMBNAIL_MAX_LENGTH);
            RuleFor(x => x.ArticlePublishedDate).GreaterThanOrEqual(0);
            RuleFor(x => x.ArticleTimeReading).GreaterThanOrEqual(0);
            RuleFor(x => x.ArticleViews).GreaterThanOrEqual(0);
            RuleFor(x => x.ArticleIPAddress).NotEmpty().MaxLength(CollectionArticlesConst.ARTICLE_IP_ADDRESS_MAX_LENGTH);
            RuleFor(x => x.ArticleBrowser).NotEmpty().MaxLength(CollectionArticlesConst.ARTICLE_BROWSER_MAX_LENGTH);
            RuleFor(x => x.ArticleOstype).MaxLength(CollectionArticlesConst.ARTICLE_OSTYPE_MAX_LENGTH);
            RuleFor(x => x.IsActived).InRange((int)ActivedType.CREATED, (int)ActivedType.ACTIVED);
            // Every tag id must be greater than 0
            RuleFor(x => x.TagIds).Must(ids => ids.All(id => id > 0));
        }
    }
}
