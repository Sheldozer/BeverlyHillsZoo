using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassLibrary.Migrations
{
    /// <inheritdoc />
    public partial class fkforrealguidevisit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Visits");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_GuideId",
                table: "Visits",
                column: "GuideId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Guides_GuideId",
                table: "Visits",
                column: "GuideId",
                principalTable: "Guides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Guides_GuideId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_GuideId",
                table: "Visits");

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Visits",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
