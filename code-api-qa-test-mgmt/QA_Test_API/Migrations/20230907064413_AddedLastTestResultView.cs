using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA_Test_Log.Migrations
{
    /// <inheritdoc />
    public partial class AddedLastTestResultView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "TestResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql(@"
create or alter view View_LastTestResult
as
SELECT t1.*
FROM TestResults t1
INNER JOIN (
    SELECT TestCaseId, MAX(Id) AS id
    FROM TestResults
    GROUP BY TestCaseId
) t2 ON t1.TestCaseId = t2.TestCaseId AND t1.Id = t2.id;

");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TestResults");
        }
    }
}
