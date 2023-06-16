using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "fd8cb5fe-7dd7-49af-9e26-4af125bf9189",
                    UserId = "9f240c9a-97a3-4c76-a1e5-4eaf8e5a2040"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "04538806-54b0-47c4-92b5-fcca4ca31b6f",
                    UserId = "a0cfb71b-46ff-49e2-90f7-d1499d87ac80"
                }
            );
        }
    }
}
