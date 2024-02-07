using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniGearRentAPI.Migrations
{
    /// <inheritdoc />
    public partial class favourites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PosterId",
                table: "UsersDetails",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "PostUserDetails",
                columns: table => new
                {
                    FavouriteIDsId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostUserDetails", x => new { x.FavouriteIDsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_PostUserDetails_Post_FavouriteIDsId",
                        column: x => x.FavouriteIDsId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostUserDetails_UsersDetails_UsersId",
                        column: x => x.UsersId,
                        principalTable: "UsersDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostUserDetails_UsersId",
                table: "PostUserDetails",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostUserDetails");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UsersDetails",
                newName: "PosterId");
        }
    }
}
