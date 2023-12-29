using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_test_project.Migrations
{
    /// <inheritdoc />
    public partial class AuthorBlogRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "BlogPages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "BlogPages");
        }
    }
}
