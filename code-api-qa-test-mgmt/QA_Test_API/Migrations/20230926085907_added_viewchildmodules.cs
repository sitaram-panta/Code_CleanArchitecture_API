using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA_Test_Log.Migrations
{
    /// <inheritdoc />
    public partial class added_viewchildmodules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneralPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralPages_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GeneralPages_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

         
            migrationBuilder.CreateIndex(
                name: "IX_GeneralPages_ModuleId",
                table: "GeneralPages",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralPages_ProjectId",
                table: "GeneralPages",
                column: "ProjectId");

            migrationBuilder.Sql(@"
CREATE VIEW dbo.View_ChildModulesList AS
SELECT
    c.Id AS Id,
    c.ModuleName AS ChildModuleName,
    c.Status AS ChildModuleStatus,
    c.ProjectId AS ChildModuleProjectId,
    c.Parent_Module_Id AS ChildModuleParentId,
    p.ModuleName AS ParentModuleName,
    CAST(CASE WHEN EXISTS (SELECT 1 FROM Modules s WHERE s.Parent_Module_Id = c.Id) THEN 1 ELSE 0 END AS bit) AS HasSubModules
FROM
    Modules c
LEFT JOIN
    Modules p ON c.Parent_Module_Id = p.Id;

");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneralPages");

            migrationBuilder.DropTable(
                name: "View_ChildModules");
        }
    }
}
