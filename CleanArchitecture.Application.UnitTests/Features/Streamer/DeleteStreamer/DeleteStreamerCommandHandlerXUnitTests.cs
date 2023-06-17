using AutoMapper;
using CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Infraestructure.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Streamer.DeleteStreamer
{
    public class DeleteStreamerCommandHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<DeleteStreamerCommandHandler>> _logger;

        public DeleteStreamerCommandHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<DeleteStreamerCommandHandler>>();

            MockStreamerRepository.AddDataStreamerRepository(_unitOfWork.Object.StreamerDbContext);
        }

        [Fact]
        public async Task DeleteStreamerCommand_InputStreamerById_ReturnsInt()
        {
            var streamerInput = new DeleteStreamerCommand(8000);

            var handler = new DeleteStreamerCommandHandler(_unitOfWork.Object, _mapper, _logger.Object);
            var result = await handler.Handle(streamerInput, CancellationToken.None);

            result.ShouldBeOfType<int>();
            result.ShouldBe(8000);
        }
    }
}
