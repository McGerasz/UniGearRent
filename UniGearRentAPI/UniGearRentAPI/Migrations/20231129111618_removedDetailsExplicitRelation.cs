using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniGearRentAPI.Migrations
{
    /// <inheritdoc />
    public partial class removedDetailsExplicitRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessorsDetails_AspNetUsers_PosterId1",
                table: "LessorsDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersDetails_AspNetUsers_PosterId1",
                table: "UsersDetails");

            migrationBuilder.DropIndex(
                name: "IX_UsersDetails_PosterId1",
                table: "UsersDetails");

            migrationBuilder.DropIndex(
                name: "IX_LessorsDetails_PosterId1",
                table: "LessorsDetails");

            migrationBuilder.DropColumn(
                name: "PosterId1",
                table: "UsersDetails");

            migrationBuilder.DropColumn(
                name: "PosterId1",
                table: "LessorsDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PosterId1",
                table: "UsersDetails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PosterId1",
                table: "LessorsDetails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UsersDetails_PosterId1",
                table: "UsersDetails",
                column: "PosterId1");

            migrationBuilder.CreateIndex(
                name: "IX_LessorsDetails_PosterId1",
                table: "LessorsDetails",
                column: "PosterId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LessorsDetails_AspNetUsers_PosterId1",
                table: "LessorsDetails",
                column: "PosterId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersDetails_AspNetUsers_PosterId1",
                table: "UsersDetails",
                column: "PosterId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
