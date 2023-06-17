using AutoMapper;
using CleanArchitecture.Application.Contracts.Infraestructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreateStreamerCommandHandler : IRequestHandler<CreateStreamerCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateStreamerCommandHandler> _logger;

        public CreateStreamerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService, ILogger<CreateStreamerCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerEntity = _mapper.Map<Streamer>(request);
            _unitOfWork.StreamerRepository.AddEntity(streamerEntity);

            var result = await _unitOfWork.Complete();

            if(result <= 0 )
            {
                _logger.LogError("Streamer could not be registered");
                throw new Exception("Streamer could not be registered");
            }

            _logger.LogInformation($"Streamer {streamerEntity.Id} was created successfully");

            await SendEmail(streamerEntity);

            return streamerEntity.Id;
        }

        public async Task SendEmail(Streamer streamer)
        {
            var email = new Email()
            {
                To = "brunorlm88@gmail.com",
                Body = $"The streaming company \"{streamer.Name}\" was created successfully",
                Subject = "Operation confirmation"
            };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception)
            {
                _logger.LogError($"Error while sending confirmation email of {streamer.Name}");
            }

        }
    }
}
