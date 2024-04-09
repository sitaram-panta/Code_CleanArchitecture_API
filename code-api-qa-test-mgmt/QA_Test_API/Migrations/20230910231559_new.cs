using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA_Test_Log.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Documents_TestResultId",
                table: "Documents");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_TestResultId",
                table: "Documents",
                column: "TestResultId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Documents_TestResultId",
                table: "Documents");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_TestResultId",
                table: "Documents",
                column: "TestResultId");
        }
    }
}
