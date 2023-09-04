using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRLeaveManagement.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "07105fba-6660-4954-971b-7d61f90e9e58",
                UserId = "d7a4a577-cfdc-44f5-aa88-72f2920e8388"
            },
            new IdentityUserRole<string>
            {
                RoleId = "c11c4384-1b5a-4493-acd4-4bcc8cc6eb89",
                UserId = "f8d1a65e-aee7-4f71-869c-4f6f93781596"
            }
            );
    }
}
