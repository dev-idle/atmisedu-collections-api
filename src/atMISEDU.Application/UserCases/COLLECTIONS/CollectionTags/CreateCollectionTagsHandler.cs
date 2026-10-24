using atMISEDU.Application.Requests.COLLECTIONS.CollectionTags;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionTags;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionTags
{
    /// <summary>
    /// Handler for <see cref="CreateCollectionTagsRequest"/>
    /// </summary>
    public class CreateCollectionTagsHandler : IRequestHandler<CreateCollectionTagsRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.CollectionTags"/>>
        private readonly ICollectionTagsSqlRepository collectionTagsRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="CreateCollectionTagsHandler"/>, inject needed dependency
        /// </summary>
        public CreateCollectionTagsHandler(ICollectionTagsSqlRepository collectionTagsRepo, ISqlUnitOfWork unitOfWork)
        {
            this.collectionTagsRepo = collectionTagsRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="CreateCollectionTagsRequest"/>, Create the <see cref="Entities.CollectionTags"/> base on data <see cref="CreateCollectionTagsRequest"/>
        /// and save to database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with id of created <see cref="Entities.CollectionTags"/></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(CreateCollectionTagsRequest request, CancellationToken cancellationToken)
        {
            // Trim string fields, then create validator and validate request
            request.TagsName = request.TagsName?.Trim();
            request.TagsDescription = request.TagsDescription?.Trim();
            request.TagsIcon = request.TagsIcon?.Trim();
            request.TagsStyle = request.TagsStyle?.Trim();
            var validator = new CreateCollectionTagsValidator();
            validator.ValidateAndThrow(request);

            // Create new CollectionTags from request, apply default values
            var collectionTags = request.MapTo<Entities.CollectionTags>();
            collectionTags.CreatedDate = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            collectionTags.IsActived = request.IsActived ?? (int)ActivedType.ACTIVED;

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CollectionTags as Create state
                collectionTagsRepo.Add(collectionTags);
                // Save data to database
                await unitOfWork.SaveChangesAsync(cancellationToken);

                // Commit transaction
                transaction.Commit();
                return Result<object>.Ok<object>(new { keyTags = collectionTags.Id });
            }
            catch (Exception)
            {
                // Rollback transaction if any exception happened, then throw exception
                transaction.Rollback();
                throw;
            }
        }
    }
}
