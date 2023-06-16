using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

#nullable disable
namespace CleanArchitecture.Infraestructure.Repositories
{
    public class VideoRepository : RepositoryBase<Video>, IVideoRepository
    {
        public VideoRepository(StreamerDbContext context) : base(context) { }

        public async Task<Video> GetVideoByName(string name)
        {
            return await _context.Videos.Where(v => v.Name == name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Video>> GetVideoByUsername(string username)
        {
            return await _context.Videos.Where(v => v.CreatedBy == username).ToListAsync();
        }
    }
}
