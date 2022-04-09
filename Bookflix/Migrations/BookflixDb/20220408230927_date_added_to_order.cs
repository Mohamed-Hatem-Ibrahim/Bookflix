using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookflix.Migrations.BookflixDb
{
    public partial class date_added_to_order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,DateTime.Now.Hour,DateTime.Now.Minute,DateTime.Now.Second));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Orders");
        }
    }
}
