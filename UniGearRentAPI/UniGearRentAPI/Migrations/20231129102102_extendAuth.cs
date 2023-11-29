using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniGearRentAPI.Migrations
{
    /// <inheritdoc />
    public partial class extendAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_AspNetUsers_PosterId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "LessorsDetails",
                columns: table => new
                {
                    PosterId = table.Column<string>(type: "text", nullable: false),
                    PosterId1 = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessorsDetails", x => x.PosterId);
                    table.ForeignKey(
                        name: "FK_LessorsDetails_AspNetUsers_PosterId1",
                        column: x => x.PosterId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersDetails",
                columns: table => new
                {
                    PosterId = table.Column<string>(type: "text", nullable: false),
                    PosterId1 = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDetails", x => x.PosterId);
                    table.ForeignKey(
                        name: "FK_UsersDetails_AspNetUsers_PosterId1",
                        column: x => x.PosterId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessorsDetails_PosterId1",
                table: "LessorsDetails",
                column: "PosterId1");

            migrationBuilder.CreateIndex(
                name: "IX_UsersDetails_PosterId1",
                table: "UsersDetails",
                column: "PosterId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_LessorsDetails_PosterId",
                table: "Post",
                column: "PosterId",
                principalTable: "LessorsDetails",
                principalColumn: "PosterId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_LessorsDetails_PosterId",
                table: "Post");

            migrationBuilder.DropTable(
                name: "LessorsDetails");

            migrationBuilder.DropTable(
                name: "UsersDetails");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_AspNetUsers_PosterId",
                table: "Post",
                column: "PosterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
