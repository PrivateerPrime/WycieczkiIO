using Microsoft.EntityFrameworkCore;
using WycieczkiIO.Models;

namespace WycieczkiIO.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            Platnosc platnosc = new Platnosc
            {
                PlatnoscId = 1,
                Kwota = 50,
                Rabat = 0,
                Status = Status.Niezaplacona
            };

            modelBuilder.Entity<Platnosc>().HasData(platnosc);

            // Wycieczka wycieczka = new Wycieczka
            // {
            //     WycieczkaId = 1,
            //     DataRozpoczecia = default,
            //     DataZakonczenia = default,
            //     DataPlatnosci = default,
            //     Status = StatusWycieczki.Aktywna,
            //     MiejsceDocelowe = "Brak",
            //     PlatnoscId = platnosc.PlatnoscId,
            //     Platnosc = null,
            //     ZakwaterowanieId = 0,
            //     Zakwaterowanie = null,
            //     Uczestnicy = null,
            //     WycieczkaAtrakcja = null,
            //     WycieczkaTransport = null
            // };
        }
    }
}