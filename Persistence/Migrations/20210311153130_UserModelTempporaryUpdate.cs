using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class UserModelTempporaryUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BibleStudies_Users_UserId",
                table: "BibleStudies");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Users_UserId1",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Sermons_Users_UserId",
                table: "Sermons");

            migrationBuilder.DropIndex(
                name: "IX_Sermons_UserId",
                table: "Sermons");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_UserId1",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BibleStudies_UserId",
                table: "BibleStudies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Sermons");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BibleStudies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Sermons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "BlogPosts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "BibleStudies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sermons_UserId",
                table: "Sermons",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_UserId1",
                table: "BlogPosts",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_BibleStudies_UserId",
                table: "BibleStudies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BibleStudies_Users_UserId",
                table: "BibleStudies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Users_UserId1",
                table: "BlogPosts",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sermons_Users_UserId",
                table: "Sermons",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
