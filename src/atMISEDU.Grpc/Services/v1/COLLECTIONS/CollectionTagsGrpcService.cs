using atMISEDU.Application.Responses.COLLECTIONS.CollectionTags;
using atMISEDU.Grpc.Extensions;
using atMISEDU.Protos.COLLECTIONS;
using atMISEDU.Protos.Common;
using Grpc.Core;
using MediatR;
using App = atMISEDU.Application.Requests.COLLECTIONS.CollectionTags;

namespace atMISEDU.Grpc.Services.v1.COLLECTIONS
{
    /// <summary>
    /// gRPC service version 1 for CollectionTags, reuse the same handlers of REST api through MediatR
    /// </summary>
    public class CollectionTagsGrpcService : CollectionTagsService.CollectionTagsServiceBase
    {
        private readonly IMediator mediator;

        public CollectionTagsGrpcService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Create CollectionTags
        /// </summary>
        public override async Task<CreateCollectionTagsReply> Create(CreateCollectionTagsRequest request, ServerCallContext context)
        {
            var command = new App.CreateCollectionTagsRequest
            {
                TagsName = request.HasTagsName ? request.TagsName : null,
                TagsDescription = request.HasTagsDescription ? request.TagsDescription : null,
                TagsIcon = request.HasTagsIcon ? request.TagsIcon : null,
                TagsStyle = request.HasTagsStyle ? request.TagsStyle : null,
                CreatedBy = request.HasCreatedBy ? request.CreatedBy : null,
                IsActived = request.HasIsActived ? request.IsActived : null
            };
            var result = await mediator.Send(command, context.CancellationToken);
            return new CreateCollectionTagsReply
            {
                Result = result.ToCommonResult(),
                KeyTags = result.GetCreatedId("keyTags")
            };
        }

        /// <summary>
        /// Update CollectionTags, only fields sent with value are changed
        /// </summary>
        public override async Task<CommonResult> Update(UpdateCollectionTagsRequest request, ServerCallContext context)
        {
            var command = new App.UpdateCollectionTagsRequest
            {
                Id = request.KeyTags,
                TagsName = request.HasTagsName ? request.TagsName : null,
                TagsDescription = request.HasTagsDescription ? request.TagsDescription : null,
                TagsIcon = request.HasTagsIcon ? request.TagsIcon : null,
                TagsStyle = request.HasTagsStyle ? request.TagsStyle : null,
                IsActived = request.HasIsActived ? request.IsActived : null
            };
            var result = await mediator.Send(command, context.CancellationToken);
            return result.ToCommonResult();
        }

        /// <summary>
        /// Soft delete CollectionTags
        /// </summary>
        public override async Task<CommonResult> Delete(DeleteCollectionTagsRequest request, ServerCallContext context)
        {
            var command = new App.DeleteCollectionTagsRequest()
            {
                Id = request.KeyTags
            };
            var result = await mediator.Send(command, context.CancellationToken);
            return result.ToCommonResult();
        }

        /// <summary>
        /// Get CollectionTags by key_tags
        /// </summary>
        public override async Task<CollectionTagsReply> GetDetail(GetDetailCollectionTagsRequest request, ServerCallContext context)
        {
            var query = new App.GetDetailCollectionTagsRequest()
            {
                Id = request.KeyTags
            };
            var result = await mediator.Send(query, context.CancellationToken);
            return new CollectionTagsReply
            {
                Result = result.ToCommonResult(),
                Data = ToData(result.Data!)
            };
        }

        /// <summary>
        /// Get list of CollectionTags, filter by is_actived when it is sent
        /// </summary>
        public override async Task<CollectionTagsListReply> GetAll(GetAllCollectionTagsRequest request, ServerCallContext context)
        {
            var query = new App.GetAllCollectionTagsRequest()
            {
                IsActived = request.HasIsActived ? request.IsActived : null
            };
            var result = await mediator.Send(query, context.CancellationToken);
            var reply = new CollectionTagsListReply { Result = result.ToCommonResult() };
            reply.Data.AddRange((result.Data ?? new List<CollectionTagsResponse>()).Select(ToData));
            return reply;
        }

        /// <summary>
        /// Convert <see cref="CollectionTagsResponse"/> to <see cref="CollectionTagsData"/>
        /// </summary>
        private static CollectionTagsData ToData(CollectionTagsResponse response)
        {
            var data = new CollectionTagsData
            {
                KeyTags = response.KeyTags,
                TagsName = response.TagsName,
                CreatedBy = response.CreatedBy,
                CreatedDate = response.CreatedDate,
                IsActived = response.IsActived
            };
            // Optional fields of proto can not be set to null, only set when database has value
            if (response.TagsDescription != null) data.TagsDescription = response.TagsDescription;
            if (response.TagsIcon != null) data.TagsIcon = response.TagsIcon;
            if (response.TagsStyle != null) data.TagsStyle = response.TagsStyle;
            return data;
        }
    }
}
