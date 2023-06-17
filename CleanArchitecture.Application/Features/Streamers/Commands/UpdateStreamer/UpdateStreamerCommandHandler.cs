using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandHandler : IRequestHandler<UpdateStreamerCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateStreamerCommandHandler> _logger;

        public UpdateStreamerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateStreamerCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerToUpdate = await _unitOfWork.StreamerRepository.FindAsync(request.Id);
            if (streamerToUpdate is null)
            {
                _logger.LogError($"Streamer with id: {request.Id} was not found");
                throw new NotFoundException(nameof(Streamer), request.Id);
            }

            _mapper.Map(request, streamerToUpdate, typeof(UpdateStreamerCommand), typeof(Streamer));

            _unitOfWork.StreamerRepository.UpdateEntity(streamerToUpdate);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                _logger.LogError("Stremer could not be updated");
                throw new Exception("Stremer could not be updated");
            }

            _logger.LogInformation($"Stremer \"{streamerToUpdate.Id}\" was updated sucessfully");

            return streamerToUpdate.Id;
        }
    }
}
