using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace salutaris.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupCreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Group",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Group_CreatorId",
                table: "Group",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Users_CreatorId",
                table: "Group",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Users_CreatorId",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Group_CreatorId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Group");
        }
    }
}
