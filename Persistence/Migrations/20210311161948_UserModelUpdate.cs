using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class UserModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFavorites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BibleStudyCount = table.Column<int>(type: "int", nullable: false),
                    SermonCount = table.Column<int>(type: "int", nullable: false),
                    BlogPostCount = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFavorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BibleStudyUserFavorite",
                columns: table => new
                {
                    BibleStudiesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserFavoritesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibleStudyUserFavorite", x => new { x.BibleStudiesId, x.UserFavoritesId });
                    table.ForeignKey(
                        name: "FK_BibleStudyUserFavorite_BibleStudies_BibleStudiesId",
                        column: x => x.BibleStudiesId,
                        principalTable: "BibleStudies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BibleStudyUserFavorite_UserFavorites_UserFavoritesId",
                        column: x => x.UserFavoritesId,
                        principalTable: "UserFavorites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostUserFavorite",
                columns: table => new
                {
                    BlogPostsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserFavoritesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostUserFavorite", x => new { x.BlogPostsId, x.UserFavoritesId });
                    table.ForeignKey(
                        name: "FK_BlogPostUserFavorite_BlogPosts_BlogPostsId",
                        column: x => x.BlogPostsId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostUserFavorite_UserFavorites_UserFavoritesId",
                        column: x => x.UserFavoritesId,
                        principalTable: "UserFavorites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SermonUserFavorite",
                columns: table => new
                {
                    SermonsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserFavoritesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SermonUserFavorite", x => new { x.SermonsId, x.UserFavoritesId });
                    table.ForeignKey(
                        name: "FK_SermonUserFavorite_Sermons_SermonsId",
                        column: x => x.SermonsId,
                        principalTable: "Sermons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SermonUserFavorite_UserFavorites_UserFavoritesId",
                        column: x => x.UserFavoritesId,
                        principalTable: "UserFavorites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BibleStudyUserFavorite_UserFavoritesId",
                table: "BibleStudyUserFavorite",
                column: "UserFavoritesId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostUserFavorite_UserFavoritesId",
                table: "BlogPostUserFavorite",
                column: "UserFavoritesId");

            migrationBuilder.CreateIndex(
                name: "IX_SermonUserFavorite_UserFavoritesId",
                table: "SermonUserFavorite",
                column: "UserFavoritesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorites_UserId",
                table: "UserFavorites",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BibleStudyUserFavorite");

            migrationBuilder.DropTable(
                name: "BlogPostUserFavorite");

            migrationBuilder.DropTable(
                name: "SermonUserFavorite");

            migrationBuilder.DropTable(
                name: "UserFavorites");
        }
    }
}
