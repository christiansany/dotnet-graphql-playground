using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_test_project.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Users_UserId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPages_Users_UserId",
                table: "BlogPages");

            migrationBuilder.DropIndex(
                name: "IX_BlogPages_UserId",
                table: "BlogPages");

            migrationBuilder.DropIndex(
                name: "IX_Authors_UserId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BlogPages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Authors");

            migrationBuilder.CreateTable(
                name: "AuthorUser",
                columns: table => new
                {
                    LikedAuthorsId = table.Column<int>(type: "INTEGER", nullable: false),
                    LikedByUsersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorUser", x => new { x.LikedAuthorsId, x.LikedByUsersId });
                    table.ForeignKey(
                        name: "FK_AuthorUser_Authors_LikedAuthorsId",
                        column: x => x.LikedAuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorUser_Users_LikedByUsersId",
                        column: x => x.LikedByUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogPageUser",
                columns: table => new
                {
                    LikedBlogPagesId = table.Column<int>(type: "INTEGER", nullable: false),
                    LikedByUsersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPageUser", x => new { x.LikedBlogPagesId, x.LikedByUsersId });
                    table.ForeignKey(
                        name: "FK_BlogPageUser_BlogPages_LikedBlogPagesId",
                        column: x => x.LikedBlogPagesId,
                        principalTable: "BlogPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPageUser_Users_LikedByUsersId",
                        column: x => x.LikedByUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorUser_LikedByUsersId",
                table: "AuthorUser",
                column: "LikedByUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPageUser_LikedByUsersId",
                table: "BlogPageUser",
                column: "LikedByUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorUser");

            migrationBuilder.DropTable(
                name: "BlogPageUser");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "BlogPages",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Authors",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPages_UserId",
                table: "BlogPages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_UserId",
                table: "Authors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Users_UserId",
                table: "Authors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPages_Users_UserId",
                table: "BlogPages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
