using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WycieczkiIO.Models
{
    public enum Typ { Hotel, Hostel, Pensjonat, Camping }
    
    public class Zakwaterowanie
    {
        public int ZakwaterowanieId { get; set; }
        [Required]
        public int AdresId { get; set; }
        public Adres Adres { get; set; }
        [Required]
        [MaxLength(60)]
        public string Nazwa { get; set; }
        [Required]
        public Typ Typ { get; set; }
        
        public ICollection<Wycieczka> Wycieczki { get; set; }
    }
}