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
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode=true)]
        [DataType(DataType.Date)]
        public DateTime DataRozpoczecia { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode=true)]
        [DataType(DataType.Date)]
        public DateTime DataZakonczenia { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode=true)]
        [DataType(DataType.Date)]
        public DateTime DataPlatnosci { get; set; }
        public StatusWycieczki Status { get; set; }
        [Required]
        [MaxLength(60)]
        public string MiejsceDocelowe { get; set; }
        
        public int PlatnoscId { get; set; }
        public Platnosc Platnosc { get; set; }
        
        public int? ZakwaterowanieId { get; set; }
        
        public virtual Zakwaterowanie Zakwaterowanie { get; set; }
        
        public ICollection<Uczestnik> Uczestnicy { get; set; }
        
        public ICollection<Atrakcja> Atrakcja { get; set; }
        
        public ICollection<WycieczkaAtrakcja> WycieczkaAtrakcja { get; set; }
        
        public ICollection<Transport> Transport { get; set; }
        
        public ICollection<WycieczkaTransport> WycieczkaTransport { get; set; }
    }
}