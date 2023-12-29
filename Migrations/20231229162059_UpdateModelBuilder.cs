using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_test_project.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelBuilder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorUser_Authors_LikedAuthorsId",
                table: "AuthorUser");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorUser_Users_LikedByUsersId",
                table: "AuthorUser");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPageUser_BlogPages_LikedBlogPagesId",
                table: "BlogPageUser");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPageUser_Users_LikedByUsersId",
                table: "BlogPageUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPageUser",
                table: "BlogPageUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorUser",
                table: "AuthorUser");

            migrationBuilder.RenameTable(
                name: "BlogPageUser",
                newName: "UserLikedBlogPages");

            migrationBuilder.RenameTable(
                name: "AuthorUser",
                newName: "UserLikedAuthors");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPageUser_LikedByUsersId",
                table: "UserLikedBlogPages",
                newName: "IX_UserLikedBlogPages_LikedByUsersId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorUser_LikedByUsersId",
                table: "UserLikedAuthors",
                newName: "IX_UserLikedAuthors_LikedByUsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLikedBlogPages",
                table: "UserLikedBlogPages",
                columns: new[] { "LikedBlogPagesId", "LikedByUsersId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLikedAuthors",
                table: "UserLikedAuthors",
                columns: new[] { "LikedAuthorsId", "LikedByUsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikedAuthors_Authors_LikedAuthorsId",
                table: "UserLikedAuthors",
                column: "LikedAuthorsId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikedAuthors_Users_LikedByUsersId",
                table: "UserLikedAuthors",
                column: "LikedByUsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikedBlogPages_BlogPages_LikedBlogPagesId",
                table: "UserLikedBlogPages",
                column: "LikedBlogPagesId",
                principalTable: "BlogPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikedBlogPages_Users_LikedByUsersId",
                table: "UserLikedBlogPages",
                column: "LikedByUsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLikedAuthors_Authors_LikedAuthorsId",
                table: "UserLikedAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikedAuthors_Users_LikedByUsersId",
                table: "UserLikedAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikedBlogPages_BlogPages_LikedBlogPagesId",
                table: "UserLikedBlogPages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikedBlogPages_Users_LikedByUsersId",
                table: "UserLikedBlogPages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLikedBlogPages",
                table: "UserLikedBlogPages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLikedAuthors",
                table: "UserLikedAuthors");

            migrationBuilder.RenameTable(
                name: "UserLikedBlogPages",
                newName: "BlogPageUser");

            migrationBuilder.RenameTable(
                name: "UserLikedAuthors",
                newName: "AuthorUser");

            migrationBuilder.RenameIndex(
                name: "IX_UserLikedBlogPages_LikedByUsersId",
                table: "BlogPageUser",
                newName: "IX_BlogPageUser_LikedByUsersId");

            migrationBuilder.RenameIndex(
                name: "IX_UserLikedAuthors_LikedByUsersId",
                table: "AuthorUser",
                newName: "IX_AuthorUser_LikedByUsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPageUser",
                table: "BlogPageUser",
                columns: new[] { "LikedBlogPagesId", "LikedByUsersId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorUser",
                table: "AuthorUser",
                columns: new[] { "LikedAuthorsId", "LikedByUsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorUser_Authors_LikedAuthorsId",
                table: "AuthorUser",
                column: "LikedAuthorsId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorUser_Users_LikedByUsersId",
                table: "AuthorUser",
                column: "LikedByUsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPageUser_BlogPages_LikedBlogPagesId",
                table: "BlogPageUser",
                column: "LikedBlogPagesId",
                principalTable: "BlogPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPageUser_Users_LikedByUsersId",
                table: "BlogPageUser",
                column: "LikedByUsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
