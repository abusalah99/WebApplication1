using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class Thrid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookEntity");

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pages = table.Column<int>(type: "int", nullable: false),
                    Release_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cover_Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.CreateTable(
                name: "BookEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ImageExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pages = table.Column<int>(type: "int", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookEntity", x => x.Id);
                });
        }
    }
}
