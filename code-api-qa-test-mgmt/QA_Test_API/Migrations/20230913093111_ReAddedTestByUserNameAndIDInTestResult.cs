using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA_Test_Log.Migrations
{
    /// <inheritdoc />
    public partial class ReAddedTestByUserNameAndIDInTestResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestedByUser",
                table: "TestResults",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TestResults",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
            migrationBuilder.Sql("exec sp_refreshview 'View_LastTestResult'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestedByUser",
                table: "TestResults");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TestResults");
        }
    }
}
