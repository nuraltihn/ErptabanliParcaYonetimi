using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erpyonetimi.Migrations
{
    /// <inheritdoc />
    public partial class SilmeIliskileri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcalar_Kategoriler_KategoriId",
                table: "Parcalar");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcalar_Tedarikciler_TedarikciId",
                table: "Parcalar");

            migrationBuilder.DropForeignKey(
                name: "FK_Raflar_Depolar_DepoId",
                table: "Raflar");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcalar_Kategoriler_KategoriId",
                table: "Parcalar",
                column: "KategoriId",
                principalTable: "Kategoriler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcalar_Tedarikciler_TedarikciId",
                table: "Parcalar",
                column: "TedarikciId",
                principalTable: "Tedarikciler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Raflar_Depolar_DepoId",
                table: "Raflar",
                column: "DepoId",
                principalTable: "Depolar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcalar_Kategoriler_KategoriId",
                table: "Parcalar");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcalar_Tedarikciler_TedarikciId",
                table: "Parcalar");

            migrationBuilder.DropForeignKey(
                name: "FK_Raflar_Depolar_DepoId",
                table: "Raflar");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcalar_Kategoriler_KategoriId",
                table: "Parcalar",
                column: "KategoriId",
                principalTable: "Kategoriler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcalar_Tedarikciler_TedarikciId",
                table: "Parcalar",
                column: "TedarikciId",
                principalTable: "Tedarikciler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Raflar_Depolar_DepoId",
                table: "Raflar",
                column: "DepoId",
                principalTable: "Depolar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
