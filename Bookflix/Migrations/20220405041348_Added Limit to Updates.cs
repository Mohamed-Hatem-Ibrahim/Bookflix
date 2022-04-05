using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookflix.Migrations
{
    public partial class AddedLimittoUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsernameChangeLimit",
                schema: "Identity",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsernameChangeLimit",
                schema: "Identity",
                table: "User");
        }
    }
}
