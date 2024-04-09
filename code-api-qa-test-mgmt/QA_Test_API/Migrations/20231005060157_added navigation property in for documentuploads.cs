using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA_Test_Log.Migrations
{
    /// <inheritdoc />
    public partial class addednavigationpropertyinfordocumentuploads : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentUpload",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: true),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentUpload", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentUpload_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentUpload_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentUpload_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentUpload_DocumentTypeId",
                table: "DocumentUpload",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentUpload_ModuleId",
                table: "DocumentUpload",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentUpload_ProjectId",
                table: "DocumentUpload",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentUpload");
        }
    }
}
