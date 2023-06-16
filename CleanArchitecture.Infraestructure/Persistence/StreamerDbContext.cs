using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Commom;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infraestructure.Persistence
{
    public class StreamerDbContext : DbContext
    {
        public StreamerDbContext(DbContextOptions<StreamerDbContext> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=PC-Bruno\SQLExpress; 
        //        Initial Catalog=Streamer; 
        //        User Id=sa; 
        //        Password=75046836Light")
        //    .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
        //    .EnableSensitiveDataLogging();

        //}

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Entity>())
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "system";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "system";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Streamer>()
                 .HasMany(m => m.Videos)
                 .WithOne(m => m.Streamer)
                 .HasForeignKey(m => m.StreamerId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Video>()
                .HasMany(m => m.Actors)
                .WithMany(m => m.Videos)
                .UsingEntity<VideoActor>(
                    pt => pt.HasKey(e => new { e.ActorId, e.VideoId })
                );

            modelBuilder.Entity<Director>()
                .HasMany(m => m.Videos)
                .WithOne(m => m.Director)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Streamer>? Streamers { get; set; }

        public DbSet<Video>? Videos { get; set; }

        public DbSet<Director>? Directors { get; set; }

        public DbSet<Actor>? Actors { get; set; }
    }
}
