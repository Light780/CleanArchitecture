using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "fd8cb5fe-7dd7-49af-9e26-4af125bf9189",
                    Name = "Administrator",
                    NormalizedName = "Administrator",
                },
                new IdentityRole
                {
                    Id = "04538806-54b0-47c4-92b5-fcca4ca31b6f",
                    Name = "Operator",
                    NormalizedName = "Operator",
                }
            );
        }
    }
}
