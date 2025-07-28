using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyNeoAcademy.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddRecentBlogPostEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecentBlogPosts",
                columns: table => new
                {
                    RecentBlogPostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompactTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecentBlogPosts", x => x.RecentBlogPostID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecentBlogPosts");
        }
    }
}
