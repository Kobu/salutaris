﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace salutaris.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueUsernameConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Name",
                table: "Users");
        }
    }
}
