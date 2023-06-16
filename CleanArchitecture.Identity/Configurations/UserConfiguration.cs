using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.HasData(
                new ApplicationUser
                {
                    Id = "9f240c9a-97a3-4c76-a1e5-4eaf8e5a2040",
                    Email = "brunorlm88@gmail.com",
                    NormalizedEmail = "brunorlm88@gmail.com",
                    Name = "Bruno",
                    LastName = "Ramos",
                    UserName = "bramos",
                    NormalizedUserName = "bramos",
                    PasswordHash = hasher.HashPassword(null!, "bramos123"),
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "a0cfb71b-46ff-49e2-90f7-d1499d87ac80",
                    Email = "light@gmail.com",
                    NormalizedEmail = "light@gmail.com",
                    Name = "Thomas",
                    LastName = "Light",
                    UserName = "drlight",
                    NormalizedUserName = "drlight",
                    PasswordHash = hasher.HashPassword(null!, "drlight123"),
                    EmailConfirmed = true
                }
            );
        }
    }
}
