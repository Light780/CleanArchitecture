using AutoMapper;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideos;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Infraestructure.Repositories;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Videos.Queries
{
    public class GetVideosQueryHandlerXUnitTests 
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;

        public GetVideosQueryHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            MockVideoRepository.AddDataVideoRepository(_unitOfWork.Object.StreamerDbContext);
        }

        [Fact]
        public async Task GetVideoListTest()
        {
            var handler = new GetVideosQueryHandler(_unitOfWork.Object, _mapper);

            var result = await handler.Handle(new GetVideosQuery("bramos"), CancellationToken.None);

            result.ShouldBeOfType<List<VideoVm>>();

            result.Count.ShouldBe(1);
        }
    }
}
