using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erpyonetimi.Migrations
{
    /// <inheritdoc />
    public partial class LogKullaniciAdi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KullaniciAdSoyad",
                table: "Loglar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KullaniciAdSoyad",
                table: "Loglar");
        }
    }
}
