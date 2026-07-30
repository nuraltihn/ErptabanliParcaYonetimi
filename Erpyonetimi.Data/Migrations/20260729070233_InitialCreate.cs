using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erpyonetimi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Depolar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Depaadi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Konum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aktifmi = table.Column<bool>(type: "bit", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depolar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aktifmi = table.Column<bool>(type: "bit", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Musteriler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MusteriKodu = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirmaAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YetkiliKisi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sehir = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VergiNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aktifmi = table.Column<bool>(type: "bit", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteriler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aktifmi = table.Column<bool>(type: "bit", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tedarikciler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TedarikciKodu = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirmaAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YetkiliKisi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VergiNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aktifmi = table.Column<bool>(type: "bit", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tedarikciler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Raflar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepoId = table.Column<int>(type: "int", nullable: false),
                    RafKodu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aktifmi = table.Column<bool>(type: "bit", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raflar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Raflar_Depolar_DepoId",
                        column: x => x.DepoId,
                        principalTable: "Depolar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Siparisler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiparisNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MusteriId = table.Column<int>(type: "int", nullable: false),
                    SiparisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToplamTutar = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Durum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aktifmi = table.Column<bool>(type: "bit", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siparisler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Siparisler_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdSoyad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    KulAd = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aktifmi = table.Column<bool>(type: "bit", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Parcalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParcaKodu = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ParcAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KategoriId = table.Column<int>(type: "int", nullable: false),
                    TedarikciId = table.Column<int>(type: "int", nullable: false),
                    Marka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Malzeme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Agirlik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uzunluk = table.Column<double>(type: "float", nullable: true),
                    Genislik = table.Column<double>(type: "float", nullable: true),
                    Yukseklik = table.Column<double>(type: "float", nullable: true),
                    Renk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlisFiyat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SatisFiyat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MevcutStok = table.Column<int>(type: "int", nullable: false),
                    MinimumStok = table.Column<int>(type: "int", nullable: false),
                    RafId = table.Column<int>(type: "int", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aktifmi = table.Column<bool>(type: "bit", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcalar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcalar_Kategoriler_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategoriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parcalar_Raflar_RafId",
                        column: x => x.RafId,
                        principalTable: "Raflar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Parcalar_Tedarikciler_TedarikciId",
                        column: x => x.TedarikciId,
                        principalTable: "Tedarikciler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SiparisDetaylari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiparisId = table.Column<int>(type: "int", nullable: false),
                    ParcaId = table.Column<int>(type: "int", nullable: false),
                    Miktar = table.Column<int>(type: "int", nullable: false),
                    BirimFiyat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ToplamFiyat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Aktifmi = table.Column<bool>(type: "bit", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiparisDetaylari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiparisDetaylari_Parcalar_ParcaId",
                        column: x => x.ParcaId,
                        principalTable: "Parcalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SiparisDetaylari_Siparisler_SiparisId",
                        column: x => x.SiparisId,
                        principalTable: "Siparisler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StokHareketleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParcaId = table.Column<int>(type: "int", nullable: false),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    DepoId = table.Column<int>(type: "int", nullable: false),
                    IslemTipi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Miktar = table.Column<int>(type: "int", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aktifmi = table.Column<bool>(type: "bit", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokHareketleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StokHareketleri_Depolar_DepoId",
                        column: x => x.DepoId,
                        principalTable: "Depolar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StokHareketleri_Parcalar_ParcaId",
                        column: x => x.ParcaId,
                        principalTable: "Parcalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StokHareketleri_Users_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Musteriler_MusteriKodu",
                table: "Musteriler",
                column: "MusteriKodu",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parcalar_KategoriId",
                table: "Parcalar",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcalar_ParcaKodu",
                table: "Parcalar",
                column: "ParcaKodu",
                unique: true,
                filter: "[ParcaKodu] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Parcalar_RafId",
                table: "Parcalar",
                column: "RafId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcalar_TedarikciId",
                table: "Parcalar",
                column: "TedarikciId");

            migrationBuilder.CreateIndex(
                name: "IX_Raflar_DepoId",
                table: "Raflar",
                column: "DepoId");

            migrationBuilder.CreateIndex(
                name: "IX_SiparisDetaylari_ParcaId",
                table: "SiparisDetaylari",
                column: "ParcaId");

            migrationBuilder.CreateIndex(
                name: "IX_SiparisDetaylari_SiparisId",
                table: "SiparisDetaylari",
                column: "SiparisId");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_MusteriId",
                table: "Siparisler",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_SiparisNo",
                table: "Siparisler",
                column: "SiparisNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StokHareketleri_DepoId",
                table: "StokHareketleri",
                column: "DepoId");

            migrationBuilder.CreateIndex(
                name: "IX_StokHareketleri_KullaniciId",
                table: "StokHareketleri",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_StokHareketleri_ParcaId",
                table: "StokHareketleri",
                column: "ParcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tedarikciler_TedarikciKodu",
                table: "Tedarikciler",
                column: "TedarikciKodu",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_KulAd",
                table: "Users",
                column: "KulAd",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RolId",
                table: "Users",
                column: "RolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiparisDetaylari");

            migrationBuilder.DropTable(
                name: "StokHareketleri");

            migrationBuilder.DropTable(
                name: "Siparisler");

            migrationBuilder.DropTable(
                name: "Parcalar");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Musteriler");

            migrationBuilder.DropTable(
                name: "Kategoriler");

            migrationBuilder.DropTable(
                name: "Raflar");

            migrationBuilder.DropTable(
                name: "Tedarikciler");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Depolar");
        }
    }
}
