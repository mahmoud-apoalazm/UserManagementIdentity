using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagementIdentity.Data.Migrations
{
    public partial class AssignAdminUserToAllRolles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [security].[UserRoles] ([UserId],[RoleId]) SELECT '4675afb2-d1d6-4769-8e8b-7278af911883',Id FROM [security].[Roles]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[UserRoles] WHERE UserId='4675afb2-d1d6-4769-8e8b-7278af911883'");
        }
    }
}
