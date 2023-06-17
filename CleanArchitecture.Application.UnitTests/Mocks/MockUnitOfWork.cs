using CleanArchitecture.Infraestructure.Persistence;
using CleanArchitecture.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CleanArchitecture.Application.UnitTests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<UnitOfWork> GetUnitOfWork()
        {
            Guid dbContextId = Guid.NewGuid();
            var options = new DbContextOptionsBuilder<StreamerDbContext>()
                .UseInMemoryDatabase($"StreamerDbContext-{Guid.NewGuid()}")
                .Options;

            var streamerDbContextFake = new StreamerDbContext(options);
            streamerDbContextFake.Database.EnsureDeleted();

            var mockUnitOfWork = new Mock<UnitOfWork>(streamerDbContextFake);

            return mockUnitOfWork;
        }
    }
}
