﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyNeoAcademy.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Comments");
        }
    }
}
