using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WycieczkiIO.Models
{
    public class Atrakcja
    {
        public int AtrakcjaId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nazwa { get; set; }
        [Required]
        public int PrzewodnikId { get; set; }
        public Przewodnik Przewodnik { get; set; }
        
        public ICollection<Wycieczka> Wycieczka { get; set; }
        
        public ICollection<WycieczkaAtrakcja> WycieczkaAtrakcja { get; set; }
    }
}