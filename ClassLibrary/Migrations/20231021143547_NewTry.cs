using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassLibrary.Migrations
{
    /// <inheritdoc />
    public partial class NewTry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "PassNumber");

            migrationBuilder.AlterColumn<int>(
                name: "PassNumber",
                table: "Visitors",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR PassNumber",
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "PassNumber");

            migrationBuilder.AlterColumn<int>(
                name: "PassNumber",
                table: "Visitors",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "NEXT VALUE FOR PassNumber");
        }
    }
}
