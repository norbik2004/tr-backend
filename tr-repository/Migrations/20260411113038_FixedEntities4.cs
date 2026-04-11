using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tr_repository.Migrations
{
    /// <inheritdoc />
    public partial class FixedEntities4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountComment",
                table: "UserPlatforms",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountUsername",
                table: "UserPlatforms",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountComment",
                table: "UserPlatforms");

            migrationBuilder.DropColumn(
                name: "AccountUsername",
                table: "UserPlatforms");
        }
    }
}
