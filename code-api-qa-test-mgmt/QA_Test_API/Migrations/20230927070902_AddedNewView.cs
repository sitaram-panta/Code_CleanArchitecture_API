using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA_Test_Log.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
if(OBJECT_ID('View_ChildModulesList', 'V') is not null)
	drop view View_ChildModulesList
");
            migrationBuilder.Sql(@"
CREATE VIEW dbo.View_ChildModulesList AS
SELECT
    cm.Id AS Id,
    cm.ModuleName AS ModuleName,
    cm.ProjectId AS ProjectId,
    cm.Parent_Module_Id AS Parent_Module_Id,
    pm.ModuleName AS ParentModuleName,
    CAST(CASE
        WHEN EXISTS (SELECT 1 FROM Modules sub WHERE sub.Parent_Module_Id = cm.Id) THEN 1
        ELSE 0
    END AS BIT) AS HasSubModules
FROM
    Modules cm
LEFT JOIN
    Modules pm ON cm.Parent_Module_Id = pm.Id;






");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_View_ChildModulesList",
                table: "View_ChildModulesList");

            migrationBuilder.RenameTable(
                name: "View_ChildModulesList",
                newName: "View_ChildModules");

            migrationBuilder.AddColumn<bool>(
                name: "ChildModuleStatus",
                table: "View_ChildModules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_View_ChildModules",
                table: "View_ChildModules",
                column: "Id");
        }
    }
}
