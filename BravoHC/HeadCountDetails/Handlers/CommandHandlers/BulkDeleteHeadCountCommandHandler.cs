using Domain.IRepositories;
using HeadCountDetails.Commands.Request;
using HeadCountDetails.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HeadCountDetails.Handlers.CommandHandlers
{
    public class BulkDeleteHeadCountCommandHandler : IRequestHandler<BulkDeleteHeadCountCommandRequest, BulkDeleteHeadCountCommandResponse>
    {
        private readonly IHeadCountRepository _repository;

        public BulkDeleteHeadCountCommandHandler(IHeadCountRepository repository)
        {
            _repository = repository;
        }

        public async Task<BulkDeleteHeadCountCommandResponse> Handle(BulkDeleteHeadCountCommandRequest request, CancellationToken cancellationToken)
        {
            var deleteResults = new List<DeleteHeadCountResult>();

            foreach (var id in request.Ids)
            {
                try
                {
                    var headCount = await _repository.GetAsync(x => x.Id == id);

                    if (headCount == null)
                    {
                        deleteResults.Add(new DeleteHeadCountResult
                        {
                            Id = id,
                            IsSuccess = false,
                            Message = $"HeadCount with ID {id} does not exist."
                        });
                        continue;
                    }

                    _repository.Remove(headCount);
                    deleteResults.Add(new DeleteHeadCountResult
                    {
                        Id = id,
                        IsSuccess = true,
                        Message = "HeadCount deleted successfully."
                    });
                }
                catch (Exception ex)
                {
                    deleteResults.Add(new DeleteHeadCountResult
                    {
                        Id = id,
                        IsSuccess = false,
                        Message = $"Error occurred while deleting headcount: {ex.Message}"
                    });
                }
            }

            await _repository.CommitAsync();

            return new BulkDeleteHeadCountCommandResponse
            {
                IsSuccess = deleteResults.All(r => r.IsSuccess),
                Results = deleteResults
            };
        }
    }
}
