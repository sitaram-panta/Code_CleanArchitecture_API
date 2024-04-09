using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA_Test_Log.Migrations
{
    /// <inheritdoc />
    public partial class addeddbset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentUpload_DocumentTypes_DocumentTypeId",
                table: "DocumentUpload");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentUpload_Modules_ModuleId",
                table: "DocumentUpload");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentUpload_projects_ProjectId",
                table: "DocumentUpload");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentUpload",
                table: "DocumentUpload");

            migrationBuilder.RenameTable(
                name: "DocumentUpload",
                newName: "DocumentUploads");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentUpload_ProjectId",
                table: "DocumentUploads",
                newName: "IX_DocumentUploads_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentUpload_ModuleId",
                table: "DocumentUploads",
                newName: "IX_DocumentUploads_ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentUpload_DocumentTypeId",
                table: "DocumentUploads",
                newName: "IX_DocumentUploads_DocumentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentUploads",
                table: "DocumentUploads",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentUploads_DocumentTypes_DocumentTypeId",
                table: "DocumentUploads",
                column: "DocumentTypeId",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentUploads_Modules_ModuleId",
                table: "DocumentUploads",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentUploads_projects_ProjectId",
                table: "DocumentUploads",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentUploads_DocumentTypes_DocumentTypeId",
                table: "DocumentUploads");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentUploads_Modules_ModuleId",
                table: "DocumentUploads");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentUploads_projects_ProjectId",
                table: "DocumentUploads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentUploads",
                table: "DocumentUploads");

            migrationBuilder.RenameTable(
                name: "DocumentUploads",
                newName: "DocumentUpload");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentUploads_ProjectId",
                table: "DocumentUpload",
                newName: "IX_DocumentUpload_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentUploads_ModuleId",
                table: "DocumentUpload",
                newName: "IX_DocumentUpload_ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentUploads_DocumentTypeId",
                table: "DocumentUpload",
                newName: "IX_DocumentUpload_DocumentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentUpload",
                table: "DocumentUpload",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentUpload_DocumentTypes_DocumentTypeId",
                table: "DocumentUpload",
                column: "DocumentTypeId",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentUpload_Modules_ModuleId",
                table: "DocumentUpload",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentUpload_projects_ProjectId",
                table: "DocumentUpload",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
