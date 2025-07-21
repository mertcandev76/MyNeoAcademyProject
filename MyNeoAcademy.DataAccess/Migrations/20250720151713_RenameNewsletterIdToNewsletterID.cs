using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyNeoAcademy.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenameNewsletterIdToNewsletterID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Newsletters",
                newName: "NewsletterID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NewsletterID",
                table: "Newsletters",
                newName: "Id");
        }
    }
}
