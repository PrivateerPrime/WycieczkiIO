using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WycieczkiIO.Models
{
    public enum Rodzaj { Autobus, Pociag, Statek, Samolot}
    
    public class Transport
    {
        public int TransportId { get; set; }
        
        [Required]
        public int AdresPoczatekId { get; set; }
        public Adres AdresPoczatek { get; set; }
        [Required]
        public int AdresKoniecId { get; set; }
        public Adres AdresKoniec { get; set; }
        [Required]
        public Rodzaj RodzajTransportu { get; set; }
        
        public ICollection<WycieczkaTransport> WycieczkaTransport { get; set; }
        
    }
}