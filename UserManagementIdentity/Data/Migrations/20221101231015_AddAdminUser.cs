using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagementIdentity.Data.Migrations
{
    public partial class AddAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePicture]) VALUES (N'4675afb2-d1d6-4769-8e8b-7278af911883', N'admin', N'ADMIN', N'admin@test.com', N'ADMIN@TEST.COM', 0, N'AQAAAAEAACcQAAAAEF/mcrIJKkCGrpbha0PU7QugFod+9bl4gxXUMgxiNAOg6IQag3Tjen5zWgcuCBA0wg==', N'VQ2NGQVU5FOETRLQL7JWHPU263R5OXK3', N'093d9bfb-20db-43c9-981a-16185c60ea1d', NULL, 0, 0, NULL, 1, 0, N'Manomb', N'banobv', NULL)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[Users] WHERE Id='4675afb2-d1d6-4769-8e8b-7278af911883' ");
        }
    }
}
