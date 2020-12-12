using Microsoft.EntityFrameworkCore.Migrations;

namespace TestProject.Persistance.Migrations
{
    public partial class Added_Link : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "SearchResultEntries",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "SearchResultEntries");
        }
    }
}
