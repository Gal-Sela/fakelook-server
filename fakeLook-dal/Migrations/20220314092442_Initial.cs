using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fakeLook_dal.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageSorce = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X_Position = table.Column<double>(type: "float", nullable: false),
                    Y_Position = table.Column<double>(type: "float", nullable: false),
                    Z_Position = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Likes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PostTag",
                columns: table => new
                {
                    PostsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTag", x => new { x.PostsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_PostTag_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTaggedPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTaggedPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTaggedPosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserTaggedPosts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CommentTag",
                columns: table => new
                {
                    CommentsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentTag", x => new { x.CommentsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_CommentTag_Comments_CommentsId",
                        column: x => x.CommentsId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTaggedComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTaggedComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTaggedComments_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserTaggedComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Content" },
                values: new object[,]
                {
                    { 1, "tag 1" },
                    { 2, "tag 2" },
                    { 3, "tag 3" },
                    { 4, "tag 4" },
                    { 5, "tag 5" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "some adress", null, "user 1", "-1220744982" },
                    { 2, "some adress", null, "user 2", "-1220744982" },
                    { 3, "some adress", null, "user 3", "-1220744982" },
                    { 4, "some adress", null, "user 4", "-1220744982" },
                    { 5, "some adress", null, "user 5", "-1220744982" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Date", "Description", "ImageSorce", "UserId", "X_Position", "Y_Position", "Z_Position" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 3, 14, 11, 24, 42, 2, DateTimeKind.Local).AddTicks(2939), "post 1", "https://s.yimg.com/ny/api/res/1.2/PPu_U6UY8JjEGaR3t4T3wQ--/YXBwaWQ9aGlnaGxhbmRlcjt3PTk2MDtoPTcyMDtjZj13ZWJw/https://s.yimg.com/uu/api/res/1.2/Rffcviow.eCHjmEu2msLJg--~B/aD0xNzU3O3c9MjM0MzthcHBpZD15dGFjaHlvbg--/https://media.zenfs.com/en/insider_articles_922/c6ce8d0b9a7b28f9c2dee8171da98b8f", 1, 33.404216600767533, 33.561727693247335, 1.2971891534144409 },
                    { 2, new DateTime(2022, 3, 14, 11, 24, 42, 2, DateTimeKind.Local).AddTicks(3015), "post 2", "https://s.yimg.com/ny/api/res/1.2/PPu_U6UY8JjEGaR3t4T3wQ--/YXBwaWQ9aGlnaGxhbmRlcjt3PTk2MDtoPTcyMDtjZj13ZWJw/https://s.yimg.com/uu/api/res/1.2/Rffcviow.eCHjmEu2msLJg--~B/aD0xNzU3O3c9MjM0MzthcHBpZD15dGFjaHlvbg--/https://media.zenfs.com/en/insider_articles_922/c6ce8d0b9a7b28f9c2dee8171da98b8f", 2, 30.180545026756711, 19.190312747993044, 13.734956189587145 },
                    { 3, new DateTime(2022, 3, 14, 11, 24, 42, 2, DateTimeKind.Local).AddTicks(3019), "post 3", "https://s.yimg.com/ny/api/res/1.2/PPu_U6UY8JjEGaR3t4T3wQ--/YXBwaWQ9aGlnaGxhbmRlcjt3PTk2MDtoPTcyMDtjZj13ZWJw/https://s.yimg.com/uu/api/res/1.2/Rffcviow.eCHjmEu2msLJg--~B/aD0xNzU3O3c9MjM0MzthcHBpZD15dGFjaHlvbg--/https://media.zenfs.com/en/insider_articles_922/c6ce8d0b9a7b28f9c2dee8171da98b8f", 3, 10.849007919417875, 31.734591461722715, 22.336472041639539 },
                    { 4, new DateTime(2022, 3, 14, 11, 24, 42, 2, DateTimeKind.Local).AddTicks(3022), "post 4", "https://s.yimg.com/ny/api/res/1.2/PPu_U6UY8JjEGaR3t4T3wQ--/YXBwaWQ9aGlnaGxhbmRlcjt3PTk2MDtoPTcyMDtjZj13ZWJw/https://s.yimg.com/uu/api/res/1.2/Rffcviow.eCHjmEu2msLJg--~B/aD0xNzU3O3c9MjM0MzthcHBpZD15dGFjaHlvbg--/https://media.zenfs.com/en/insider_articles_922/c6ce8d0b9a7b28f9c2dee8171da98b8f", 4, 8.1486775281687187, 35.798331998854842, 44.889914655062348 },
                    { 5, new DateTime(2022, 3, 14, 11, 24, 42, 2, DateTimeKind.Local).AddTicks(3026), "post 5", "https://s.yimg.com/ny/api/res/1.2/PPu_U6UY8JjEGaR3t4T3wQ--/YXBwaWQ9aGlnaGxhbmRlcjt3PTk2MDtoPTcyMDtjZj13ZWJw/https://s.yimg.com/uu/api/res/1.2/Rffcviow.eCHjmEu2msLJg--~B/aD0xNzU3O3c9MjM0MzthcHBpZD15dGFjaHlvbg--/https://media.zenfs.com/en/insider_articles_922/c6ce8d0b9a7b28f9c2dee8171da98b8f", 5, 42.684337256724838, 0.48843197986596087, 12.179258485818266 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "PostId", "UserId" },
                values: new object[,]
                {
                    { 1, "comment 1", 1, 1 },
                    { 2, "comment 2", 1, 2 },
                    { 3, "comment 3", 1, 3 },
                    { 4, "comment 4", 1, 4 },
                    { 5, "comment 5", 1, 5 },
                    { 6, "comment 1", 2, 1 },
                    { 7, "comment 2", 2, 2 },
                    { 8, "comment 3", 2, 3 },
                    { 9, "comment 4", 2, 4 },
                    { 10, "comment 5", 2, 5 },
                    { 11, "comment 1", 3, 1 },
                    { 12, "comment 2", 3, 2 },
                    { 13, "comment 3", 3, 3 },
                    { 14, "comment 4", 3, 4 },
                    { 15, "comment 5", 3, 5 },
                    { 16, "comment 1", 4, 1 },
                    { 17, "comment 2", 4, 2 },
                    { 18, "comment 3", 4, 3 },
                    { 19, "comment 4", 4, 4 },
                    { 20, "comment 5", 4, 5 },
                    { 21, "comment 1", 5, 1 },
                    { 22, "comment 2", 5, 2 },
                    { 23, "comment 3", 5, 3 },
                    { 24, "comment 4", 5, 4 },
                    { 25, "comment 5", 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "Likes",
                columns: new[] { "Id", "IsActive", "PostId", "UserId" },
                values: new object[,]
                {
                    { 1, true, 1, 1 },
                    { 2, true, 1, 2 },
                    { 3, true, 1, 3 },
                    { 4, true, 1, 4 },
                    { 5, true, 1, 5 },
                    { 6, true, 2, 1 },
                    { 7, true, 2, 2 },
                    { 8, true, 2, 3 },
                    { 9, true, 2, 4 },
                    { 10, true, 2, 5 },
                    { 11, true, 3, 1 },
                    { 12, true, 3, 2 },
                    { 13, true, 3, 3 },
                    { 14, true, 3, 4 },
                    { 15, true, 3, 5 },
                    { 16, true, 4, 1 },
                    { 17, true, 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "Likes",
                columns: new[] { "Id", "IsActive", "PostId", "UserId" },
                values: new object[,]
                {
                    { 18, true, 4, 3 },
                    { 19, true, 4, 4 },
                    { 20, true, 4, 5 },
                    { 21, true, 5, 1 },
                    { 22, true, 5, 2 },
                    { 23, true, 5, 3 },
                    { 24, true, 5, 4 },
                    { 25, true, 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "UserTaggedPosts",
                columns: new[] { "Id", "PostId", "UserId" },
                values: new object[,]
                {
                    { 1, 5, 1 },
                    { 2, 4, 2 },
                    { 3, 3, 3 },
                    { 4, 2, 4 },
                    { 5, 1, 5 }
                });

            migrationBuilder.InsertData(
                table: "UserTaggedComments",
                columns: new[] { "Id", "CommentId", "UserId" },
                values: new object[,]
                {
                    { 1, 18, 4 },
                    { 2, 24, 5 },
                    { 3, 18, 4 },
                    { 4, 11, 2 },
                    { 5, 2, 5 },
                    { 6, 7, 4 },
                    { 7, 1, 2 },
                    { 8, 21, 1 },
                    { 9, 9, 4 },
                    { 10, 2, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentTag_TagsId",
                table: "CommentTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_PostId",
                table: "Likes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_TagsId",
                table: "PostTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTaggedComments_CommentId",
                table: "UserTaggedComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTaggedComments_UserId",
                table: "UserTaggedComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTaggedPosts_PostId",
                table: "UserTaggedPosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTaggedPosts_UserId",
                table: "UserTaggedPosts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentTag");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "PostTag");

            migrationBuilder.DropTable(
                name: "UserTaggedComments");

            migrationBuilder.DropTable(
                name: "UserTaggedPosts");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
