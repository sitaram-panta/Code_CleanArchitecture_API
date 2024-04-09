using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA_Test_Log.Migrations
{
    /// <inheritdoc />
    public partial class AddedDocumenttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResults_Users_UserId",
                table: "TestResults");

            migrationBuilder.DropIndex(
                name: "IX_TestResults_UserId",
                table: "TestResults");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TestedByUser",
                table: "TestResults");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TestResults");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TestedByUser",
                table: "TestResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TestResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_UserId",
                table: "TestResults",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResults_Users_UserId",
                table: "TestResults",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
