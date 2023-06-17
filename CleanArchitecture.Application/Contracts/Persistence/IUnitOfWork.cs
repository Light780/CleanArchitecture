using CleanArchitecture.Domain.Commom;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        // Custom Repositories
        IStreamerRepository StreamerRepository { get; }
        IVideoRepository VideoRepository { get; }

        // Methods
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : Entity;

        Task<int> Complete();
    }
}
