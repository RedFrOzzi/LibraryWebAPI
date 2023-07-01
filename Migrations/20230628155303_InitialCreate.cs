using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookReaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookReaders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookStacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookStacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    BookStackId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_BookStacks_BookStackId",
                        column: x => x.BookStackId,
                        principalTable: "BookStacks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CurrentReaderId = table.Column<int>(type: "INTEGER", nullable: true),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: true),
                    BookStackId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Books_BookReaders_CurrentReaderId",
                        column: x => x.CurrentReaderId,
                        principalTable: "BookReaders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Books_BookStacks_BookStackId",
                        column: x => x.BookStackId,
                        principalTable: "BookStacks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_BookStackId",
                table: "Authors",
                column: "BookStackId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookStackId",
                table: "Books",
                column: "BookStackId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CurrentReaderId",
                table: "Books",
                column: "CurrentReaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "BookReaders");

            migrationBuilder.DropTable(
                name: "BookStacks");
        }
    }
}
