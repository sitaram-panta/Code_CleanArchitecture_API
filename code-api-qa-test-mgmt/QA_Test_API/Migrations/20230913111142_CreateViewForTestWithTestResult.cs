using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA_Test_Log.Migrations
{
    /// <inheritdoc />
    public partial class CreateViewForTestWithTestResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
create or alter view dbo.View_TestCaseWithLastResult
as
    select t1.*, m1.ModuleName, p1.Id ProjectId, p1.Title ProjectName,
    v1.Id LastTestResultId, v1.Status LastTestResultStatus, 
    v1.ActualResult LastTestResultActualResult, v1.TestedOn LastTestResultTestedOn
    from TestCases t1
    left join Modules m1 on m1.Id = t1.ModuleId
    left join projects p1 on p1.Id = m1.ProjectId
    left join View_LastTestResult v1
    on t1.Id = v1.TestCaseId
");
            //migrationBuilder.CreateTable(
            //    name: "View_TestCaseWithLastResult",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        TestTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Scenario = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PreRequisities = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        TestSteps = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        TestData = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ExpectedResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Status = table.Column<bool>(type: "bit", nullable: false),
            //        ModuleId = table.Column<int>(type: "int", nullable: false),
            //        ModuleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ProjectId = table.Column<int>(type: "int", nullable: false),
            //        ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastTestResultId = table.Column<int>(type: "int", nullable: true),
            //        LastTestResultStatus = table.Column<bool>(type: "bit", nullable: true),
            //        LastTestResultActualResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastTestResultTestedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_View_TestCaseWithLastResult", x => x.Id);
            //    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "View_TestCaseWithLastResult");
        }
    }
}
