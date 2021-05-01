using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class NewInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BibleStudies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BibleStudyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BibleStudyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BibleStudyVideoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BibleStudyViews = table.Column<int>(type: "int", nullable: false),
                    BibleStudyShares = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibleStudies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogPostCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChurchEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChurchEventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChurchEventDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChurchEventDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChurchEventFacebookLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChurchEventImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChurchEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SermonSeries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeriesName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SermonSeries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    CanBlog = table.Column<bool>(type: "bit", nullable: false),
                    CanRecieveNotifications = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sermons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SermonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SermonDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SermonVideoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SermonViews = table.Column<int>(type: "int", nullable: false),
                    SermonShares = table.Column<int>(type: "int", nullable: false),
                    SermonSeriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sermons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sermons_SermonSeries_SermonSeriesId",
                        column: x => x.SermonSeriesId,
                        principalTable: "SermonSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogPostTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogPostContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogPostImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BlogPostViews = table.Column<int>(type: "int", nullable: false),
                    BlogPostShares = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPosts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserFavorites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BibleStudyCount = table.Column<int>(type: "int", nullable: false),
                    SermonCount = table.Column<int>(type: "int", nullable: false),
                    BlogPostCount = table.Column<int>(type: "int", nullable: false),
                    ChurchEventsCount = table.Column<int>(type: "int", nullable: false),
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
                name: "BlogComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogComments_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostBlogPostCategory",
                columns: table => new
                {
                    BlogCategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogPostsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostBlogPostCategory", x => new { x.BlogCategoriesId, x.BlogPostsId });
                    table.ForeignKey(
                        name: "FK_BlogPostBlogPostCategory_BlogPostCategories_BlogCategoriesId",
                        column: x => x.BlogCategoriesId,
                        principalTable: "BlogPostCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostBlogPostCategory_BlogPosts_BlogPostsId",
                        column: x => x.BlogPostsId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "ChurchEventUserFavorite",
                columns: table => new
                {
                    ChurchEventsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserFavoritesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChurchEventUserFavorite", x => new { x.ChurchEventsId, x.UserFavoritesId });
                    table.ForeignKey(
                        name: "FK_ChurchEventUserFavorite_ChurchEvents_ChurchEventsId",
                        column: x => x.ChurchEventsId,
                        principalTable: "ChurchEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChurchEventUserFavorite_UserFavorites_UserFavoritesId",
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
                name: "IX_BlogComments_BlogPostId",
                table: "BlogComments",
                column: "BlogPostId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_UserId",
                table: "BlogComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostBlogPostCategory_BlogPostsId",
                table: "BlogPostBlogPostCategory",
                column: "BlogPostsId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_UserId",
                table: "BlogPosts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostUserFavorite_UserFavoritesId",
                table: "BlogPostUserFavorite",
                column: "UserFavoritesId");

            migrationBuilder.CreateIndex(
                name: "IX_ChurchEventUserFavorite_UserFavoritesId",
                table: "ChurchEventUserFavorite",
                column: "UserFavoritesId");

            migrationBuilder.CreateIndex(
                name: "IX_Sermons_SermonSeriesId",
                table: "Sermons",
                column: "SermonSeriesId");

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
                name: "BlogComments");

            migrationBuilder.DropTable(
                name: "BlogPostBlogPostCategory");

            migrationBuilder.DropTable(
                name: "BlogPostUserFavorite");

            migrationBuilder.DropTable(
                name: "ChurchEventUserFavorite");

            migrationBuilder.DropTable(
                name: "SermonUserFavorite");

            migrationBuilder.DropTable(
                name: "BibleStudies");

            migrationBuilder.DropTable(
                name: "BlogPostCategories");

            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "ChurchEvents");

            migrationBuilder.DropTable(
                name: "Sermons");

            migrationBuilder.DropTable(
                name: "UserFavorites");

            migrationBuilder.DropTable(
                name: "SermonSeries");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
