using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erpyonetimi.Migrations
{
    /// <inheritdoc />
    public partial class RafParcaDeleteRestrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcalar_Raflar_RafId",
                table: "Parcalar");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcalar_Raflar_RafId",
                table: "Parcalar",
                column: "RafId",
                principalTable: "Raflar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcalar_Raflar_RafId",
                table: "Parcalar");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcalar_Raflar_RafId",
                table: "Parcalar",
                column: "RafId",
                principalTable: "Raflar",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
