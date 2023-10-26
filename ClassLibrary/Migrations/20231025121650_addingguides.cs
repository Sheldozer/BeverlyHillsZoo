using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassLibrary.Migrations
{
    /// <inheritdoc />
    public partial class addingguides : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Guides",
                newName: "FirstName");

            migrationBuilder.CreateSequence<int>(
                name: "GuideNumber");

            migrationBuilder.AddColumn<int>(
                name: "GuideNumber",
                table: "Guides",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR GuideNumber");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Guides",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuideNumber",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Guides");

            migrationBuilder.DropSequence(
                name: "GuideNumber");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Guides",
                newName: "Name");
        }
    }
}
