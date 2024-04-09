using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA_Test_Log.Migrations
{
    /// <inheritdoc />
    public partial class addedpropindocumentuploadmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "DocumentUploads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "DocumentUploads",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "DocumentUploads");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "DocumentUploads");
        }
    }
}
