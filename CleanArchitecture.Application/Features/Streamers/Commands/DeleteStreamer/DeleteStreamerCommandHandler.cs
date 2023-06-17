using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer
{
    public class DeleteStreamerCommandHandler : IRequestHandler<DeleteStreamerCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteStreamerCommandHandler> _logger;

        public DeleteStreamerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteStreamerCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerToDelete = await _unitOfWork.StreamerRepository.FindAsync(request.Id);
            if(streamerToDelete is null)
            {
                _logger.LogError($"Streamer with id: {request.Id} was not found");
                throw new NotFoundException(nameof(Streamer), request.Id);
            }

            _unitOfWork.StreamerRepository.DeleteEntity(streamerToDelete);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                _logger.LogError("Stremer could not be deleted");
                throw new Exception("Stremer could not be deleted");
            }

            _logger.LogInformation($"Stremer \"{streamerToDelete.Id}\" was deleted sucessfully");

            return streamerToDelete.Id;
        }
    }
}
