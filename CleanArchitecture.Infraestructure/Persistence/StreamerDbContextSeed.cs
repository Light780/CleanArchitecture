using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infraestructure.Persistence
{
    public class StreamerDbContextSeed
    {
        public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContextSeed> logger)
        {
            if (!context.Streamers!.Any())
            {
                context.Streamers!.AddRange(GetPreconfiguredStreamer());
                await context.SaveChangesAsync();
                logger.LogInformation("Data seeded successfully");
            }
        }

        private static IEnumerable<Streamer> GetPreconfiguredStreamer()
        {
            return new List<Streamer>()
            {
                new Streamer { CreatedBy = "bramos", Name = "Youtube", Url = "http://youtube.com" },
                new Streamer { CreatedBy = "bramos", Name = "Amazon Prime", Url = "http://amazonprime.com" },
                new Streamer { CreatedBy = "bramos", Name = "Star Plus", Url = "http://starplus.com" },
            };
        }
    }
}
