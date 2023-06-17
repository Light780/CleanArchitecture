using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Commom;
using CleanArchitecture.Infraestructure.Persistence;
using System.Collections;

namespace CleanArchitecture.Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories = null!;
        private readonly StreamerDbContext _context;

        // Custom Repositories
        private IVideoRepository _videoRepository = null!;
        private IStreamerRepository _streamRepository = null!;

        public IVideoRepository VideoRepository => _videoRepository ??= new VideoRepository(_context);
        public IStreamerRepository StreamerRepository => _streamRepository ??= new StreamerRepository(_context);

        public UnitOfWork(StreamerDbContext context)
        {
            _context = context;
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : Entity
        {
            if(_repositories is null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if(!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (IAsyncRepository<TEntity>)_repositories[type]!;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
