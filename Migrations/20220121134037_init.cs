using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WycieczkiIO.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kraj",
                columns: table => new
                {
                    KrajId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazwaKraju = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kraj", x => x.KrajId);
                });

            migrationBuilder.CreateTable(
                name: "Miasto",
                columns: table => new
                {
                    MiastoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazwaMiasta = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Miasto", x => x.MiastoId);
                });

            migrationBuilder.CreateTable(
                name: "Platnosc",
                columns: table => new
                {
                    PlatnoscId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kwota = table.Column<double>(type: "float", nullable: false),
                    Rabat = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platnosc", x => x.PlatnoscId);
                });

            migrationBuilder.CreateTable(
                name: "Przewodnik",
                columns: table => new
                {
                    PrzewodnikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Przewodnik", x => x.PrzewodnikId);
                });

            migrationBuilder.CreateTable(
                name: "Adres",
                columns: table => new
                {
                    AdresId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ulica = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Numer = table.Column<int>(type: "int", nullable: false),
                    KodPocztowy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MiastoId = table.Column<int>(type: "int", nullable: false),
                    KrajId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adres", x => x.AdresId);
                    table.ForeignKey(
                        name: "FK_Adres_Kraj_KrajId",
                        column: x => x.KrajId,
                        principalTable: "Kraj",
                        principalColumn: "KrajId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Adres_Miasto_MiastoId",
                        column: x => x.MiastoId,
                        principalTable: "Miasto",
                        principalColumn: "MiastoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Atrakcja",
                columns: table => new
                {
                    AtrakcjaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PrzewodnikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atrakcja", x => x.AtrakcjaId);
                    table.ForeignKey(
                        name: "FK_Atrakcja_Przewodnik_PrzewodnikId",
                        column: x => x.PrzewodnikId,
                        principalTable: "Przewodnik",
                        principalColumn: "PrzewodnikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transport",
                columns: table => new
                {
                    TransportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdresPoczatekId = table.Column<int>(type: "int", nullable: false),
                    AdresKoniecId = table.Column<int>(type: "int", nullable: false),
                    RodzajTransportu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transport", x => x.TransportId);
                    table.ForeignKey(
                        name: "FK_Transport_Adres_AdresKoniecId",
                        column: x => x.AdresKoniecId,
                        principalTable: "Adres",
                        principalColumn: "AdresId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Transport_Adres_AdresPoczatekId",
                        column: x => x.AdresPoczatekId,
                        principalTable: "Adres",
                        principalColumn: "AdresId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Zakwaterowanie",
                columns: table => new
                {
                    ZakwaterowanieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdresId = table.Column<int>(type: "int", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Typ = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zakwaterowanie", x => x.ZakwaterowanieId);
                    table.ForeignKey(
                        name: "FK_Zakwaterowanie_Adres_AdresId",
                        column: x => x.AdresId,
                        principalTable: "Adres",
                        principalColumn: "AdresId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wycieczka",
                columns: table => new
                {
                    WycieczkaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataRozpoczecia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataZakonczenia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataPlatnosci = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    MiejsceDocelowe = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    PlatnoscId = table.Column<int>(type: "int", nullable: false),
                    ZakwaterowanieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wycieczka", x => x.WycieczkaId);
                    table.ForeignKey(
                        name: "FK_Wycieczka_Platnosc_PlatnoscId",
                        column: x => x.PlatnoscId,
                        principalTable: "Platnosc",
                        principalColumn: "PlatnoscId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wycieczka_Zakwaterowanie_ZakwaterowanieId",
                        column: x => x.ZakwaterowanieId,
                        principalTable: "Zakwaterowanie",
                        principalColumn: "ZakwaterowanieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AtrakcjaWycieczka",
                columns: table => new
                {
                    AtrakcjaId = table.Column<int>(type: "int", nullable: false),
                    WycieczkaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtrakcjaWycieczka", x => new { x.AtrakcjaId, x.WycieczkaId });
                    table.ForeignKey(
                        name: "FK_AtrakcjaWycieczka_Atrakcja_AtrakcjaId",
                        column: x => x.AtrakcjaId,
                        principalTable: "Atrakcja",
                        principalColumn: "AtrakcjaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AtrakcjaWycieczka_Wycieczka_WycieczkaId",
                        column: x => x.WycieczkaId,
                        principalTable: "Wycieczka",
                        principalColumn: "WycieczkaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransportWycieczka",
                columns: table => new
                {
                    TransportId = table.Column<int>(type: "int", nullable: false),
                    WycieczkasWycieczkaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportWycieczka", x => new { x.TransportId, x.WycieczkasWycieczkaId });
                    table.ForeignKey(
                        name: "FK_TransportWycieczka_Transport_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Transport",
                        principalColumn: "TransportId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransportWycieczka_Wycieczka_WycieczkasWycieczkaId",
                        column: x => x.WycieczkasWycieczkaId,
                        principalTable: "Wycieczka",
                        principalColumn: "WycieczkaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uczestnik",
                columns: table => new
                {
                    UczestnikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    WycieczkiId = table.Column<int>(type: "int", nullable: false),
                    WycieczkaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uczestnik", x => x.UczestnikId);
                    table.ForeignKey(
                        name: "FK_Uczestnik_Wycieczka_WycieczkaId",
                        column: x => x.WycieczkaId,
                        principalTable: "Wycieczka",
                        principalColumn: "WycieczkaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WycieczkaAtrakcja",
                columns: table => new
                {
                    AtrakcjaId = table.Column<int>(type: "int", nullable: false),
                    WycieczkaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WycieczkaAtrakcja", x => new { x.WycieczkaId, x.AtrakcjaId });
                    table.ForeignKey(
                        name: "FK_WycieczkaAtrakcja_Atrakcja_AtrakcjaId",
                        column: x => x.AtrakcjaId,
                        principalTable: "Atrakcja",
                        principalColumn: "AtrakcjaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WycieczkaAtrakcja_Wycieczka_WycieczkaId",
                        column: x => x.WycieczkaId,
                        principalTable: "Wycieczka",
                        principalColumn: "WycieczkaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WycieczkaTransport",
                columns: table => new
                {
                    TransportId = table.Column<int>(type: "int", nullable: false),
                    WycieczkaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WycieczkaTransport", x => new { x.WycieczkaId, x.TransportId });
                    table.ForeignKey(
                        name: "FK_WycieczkaTransport_Transport_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Transport",
                        principalColumn: "TransportId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WycieczkaTransport_Wycieczka_WycieczkaId",
                        column: x => x.WycieczkaId,
                        principalTable: "Wycieczka",
                        principalColumn: "WycieczkaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Platnosc",
                columns: new[] { "PlatnoscId", "Kwota", "Rabat", "Status" },
                values: new object[] { 1, 50.0, 0.0, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Adres_KrajId",
                table: "Adres",
                column: "KrajId");

            migrationBuilder.CreateIndex(
                name: "IX_Adres_MiastoId",
                table: "Adres",
                column: "MiastoId");

            migrationBuilder.CreateIndex(
                name: "IX_Atrakcja_PrzewodnikId",
                table: "Atrakcja",
                column: "PrzewodnikId");

            migrationBuilder.CreateIndex(
                name: "IX_AtrakcjaWycieczka_WycieczkaId",
                table: "AtrakcjaWycieczka",
                column: "WycieczkaId");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_AdresKoniecId",
                table: "Transport",
                column: "AdresKoniecId");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_AdresPoczatekId",
                table: "Transport",
                column: "AdresPoczatekId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportWycieczka_WycieczkasWycieczkaId",
                table: "TransportWycieczka",
                column: "WycieczkasWycieczkaId");

            migrationBuilder.CreateIndex(
                name: "IX_Uczestnik_WycieczkaId",
                table: "Uczestnik",
                column: "WycieczkaId");

            migrationBuilder.CreateIndex(
                name: "IX_Wycieczka_PlatnoscId",
                table: "Wycieczka",
                column: "PlatnoscId");

            migrationBuilder.CreateIndex(
                name: "IX_Wycieczka_ZakwaterowanieId",
                table: "Wycieczka",
                column: "ZakwaterowanieId");

            migrationBuilder.CreateIndex(
                name: "IX_WycieczkaAtrakcja_AtrakcjaId",
                table: "WycieczkaAtrakcja",
                column: "AtrakcjaId");

            migrationBuilder.CreateIndex(
                name: "IX_WycieczkaTransport_TransportId",
                table: "WycieczkaTransport",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_Zakwaterowanie_AdresId",
                table: "Zakwaterowanie",
                column: "AdresId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtrakcjaWycieczka");

            migrationBuilder.DropTable(
                name: "TransportWycieczka");

            migrationBuilder.DropTable(
                name: "Uczestnik");

            migrationBuilder.DropTable(
                name: "WycieczkaAtrakcja");

            migrationBuilder.DropTable(
                name: "WycieczkaTransport");

            migrationBuilder.DropTable(
                name: "Atrakcja");

            migrationBuilder.DropTable(
                name: "Transport");

            migrationBuilder.DropTable(
                name: "Wycieczka");

            migrationBuilder.DropTable(
                name: "Przewodnik");

            migrationBuilder.DropTable(
                name: "Platnosc");

            migrationBuilder.DropTable(
                name: "Zakwaterowanie");

            migrationBuilder.DropTable(
                name: "Adres");

            migrationBuilder.DropTable(
                name: "Kraj");

            migrationBuilder.DropTable(
                name: "Miasto");
        }
    }
}
