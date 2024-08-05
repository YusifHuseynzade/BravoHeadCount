﻿using Core.Helpers;
using Domain.IRepositories;
using MediatR;
using StoreDetails.Commands.Request;
using StoreDetails.Commands.Response;

namespace StoreDetails.Handlers.CommandHandlers
{
    public class UpdateStoreCommandHandler : IRequestHandler<UpdateStoreCommandRequest, UpdateStoreCommandResponse>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IFunctionalAreaRepository _functionalAreaRepository;
        private readonly IFormatRepository _formatRepository;

        public UpdateStoreCommandHandler(
            IStoreRepository storeRepository,
            IProjectRepository projectRepository,
            IFunctionalAreaRepository functionalAreaRepository,
            IFormatRepository formatRepository)
        {
            _storeRepository = storeRepository;
            _projectRepository = projectRepository;
            _functionalAreaRepository = functionalAreaRepository;
            _formatRepository = formatRepository;
        }

        public async Task<UpdateStoreCommandResponse> Handle(UpdateStoreCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Validasyon
                if (request.Id <= 0)
                    throw new BadRequestException("Id is required and must be greater than 0.");

                if (request.ProjectId <= 0)
                    throw new BadRequestException("ProjectId is required and must be greater than 0.");

                if (request.FunctionalAreaId <= 0)
                    throw new BadRequestException("FunctionalAreaId is required and must be greater than 0.");

                if (request.FormatId <= 0)
                    throw new BadRequestException("FormatId is required and must be greater than 0.");

                if (request.HeadCountNumber < 0)
                    throw new BadRequestException("HeadCountNumber is required and must not be negative.");

                // Veritabanı kontrolü
                var storeExists = await _storeRepository.IsExistAsync(d => d.Id == request.Id);
                if (!storeExists)
                    throw new BadRequestException($"Store with ID {request.Id} does not exist.");

                var projectExists = await _projectRepository.IsExistAsync(d => d.Id == request.ProjectId);
                if (!projectExists)
                    throw new BadRequestException($"Project with ID {request.ProjectId} does not exist.");

                var functionalAreaExists = await _functionalAreaRepository.IsExistAsync(d => d.Id == request.FunctionalAreaId);
                if (!functionalAreaExists)
                    throw new BadRequestException($"FunctionalArea with ID {request.FunctionalAreaId} does not exist.");

                var formatExists = await _formatRepository.IsExistAsync(d => d.Id == request.FormatId);
                if (!formatExists)
                    throw new BadRequestException($"Format with ID {request.FormatId} does not exist.");

                var store = await _storeRepository.GetAsync(d => d.Id == request.Id);
                if (store == null)
                {
                    throw new BadRequestException($"Store with ID {request.Id} does not exist.");
                }

                store.ProjectId = request.ProjectId;
                store.FunctionalAreaId = request.FunctionalAreaId;
                store.FormatId = request.FormatId;
                store.HeadCountNumber = request.HeadCountNumber;
                store.ModifiedDate = DateTime.UtcNow;

                await _storeRepository.UpdateAsync(store);

                return new UpdateStoreCommandResponse
                {
                    IsSuccess = true,
                    Message = "Store updated successfully."
                };
            }
            catch (ValidationException valEx)
            {
                return new UpdateStoreCommandResponse
                {
                    IsSuccess = false,
                    Message = $"BadRequest: {valEx.Message}"
                };
            }
            catch (Exception ex)
            {
                return new UpdateStoreCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Error occurred while updating store: {ex.Message}"
                };
            }
        }
    }
}
