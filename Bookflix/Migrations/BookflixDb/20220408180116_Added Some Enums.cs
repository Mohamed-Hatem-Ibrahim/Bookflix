using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookflix.Migrations.BookflixDb
{
    public partial class AddedSomeEnums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookType",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoverType",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PublishingType",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookType",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CoverType",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PublishingType",
                table: "Books");
        }
    }
}
