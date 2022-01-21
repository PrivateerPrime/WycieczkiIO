using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WycieczkiIO.Models
{
    public class Przewodnik
    {
        public int PrzewodnikId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Imie { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nazwisko { get; set; }
        [Required]
        [MaxLength(30)]
        public string Telefon { get; set; }
        public ICollection<Atrakcja> Atrakcja { get; set; }
    }
}