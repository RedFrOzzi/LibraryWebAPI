using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedBookStackRef : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookStacks_BookStackId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "BookStackId",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookStacks_BookStackId",
                table: "Books",
                column: "BookStackId",
                principalTable: "BookStacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookStacks_BookStackId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "BookStackId",
                table: "Books",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookStacks_BookStackId",
                table: "Books",
                column: "BookStackId",
                principalTable: "BookStacks",
                principalColumn: "Id");
        }
    }
}
