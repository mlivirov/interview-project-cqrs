using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestProject.Persistance.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SearchPhrase = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusUpdated = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchResultEntries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    SearchRequestId = table.Column<long>(type: "INTEGER", nullable: true),
                    SearchEngine = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchResultEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchResultEntries_SearchRequests_SearchRequestId",
                        column: x => x.SearchRequestId,
                        principalTable: "SearchRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SearchResultEntries_SearchRequestId",
                table: "SearchResultEntries",
                column: "SearchRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchResultEntries");

            migrationBuilder.DropTable(
                name: "SearchRequests");
        }
    }
}
