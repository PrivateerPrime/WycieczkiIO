using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WycieczkiIO.Models
{
    public enum StatusWycieczki { Aktywna, OczekiwanaZaplata, Anulowana}
    
    public class Wycieczka
    {
        public int WycieczkaId { get; set; }
        [Required]
        public DateTime DataRozpoczecia { get; set; }
        [Required]
        public DateTime DataZakonczenia { get; set; }
        [Required]
        public DateTime DataPlatnosci { get; set; }
        [Required]
        public StatusWycieczki Status { get; set; }
        [Required]
        [MaxLength(60)]
        public string MiejsceDocelowe { get; set; }
        [Required]
        public int PlatnoscId { get; set; }
        [Required]
        public Platnosc Platnosc { get; set; }
        
        public int ZakwaterowanieId { get; set; }
        
        public Zakwaterowanie Zakwaterowanie { get; set; }
        
        public ICollection<Uczestnik> Uczestnicy { get; set; }
        
        public ICollection<WycieczkaAtrakcja> WycieczkaAtrakcja { get; set; }
        
        public ICollection<WycieczkaTransport> WycieczkaTransport { get; set; }
    }
}