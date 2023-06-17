using AutoMapper;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Infraestructure.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Streamer.UpdateStreamer
{
    public class UpdateStreamerCommandHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<UpdateStreamerCommandHandler>> _logger;

        public UpdateStreamerCommandHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<UpdateStreamerCommandHandler>>();

            MockStreamerRepository.AddDataStreamerRepository(_unitOfWork.Object.StreamerDbContext);
        }

        [Fact]
        public async Task UpdateStreamerCommand_InputStreamer_ReturnsInt()
        {
            var streamerInput = new UpdateStreamerCommand
            {
                Id = 8000,
                Name = "Netflix 2",
                Url = "https://netflix.com"
            };

            var handler = new UpdateStreamerCommandHandler(_unitOfWork.Object, _mapper, _logger.Object);
            var result = await handler.Handle(streamerInput, CancellationToken.None);

            result.ShouldBeOfType<int>();
            result.ShouldBe(8000);
        }
    }
}
