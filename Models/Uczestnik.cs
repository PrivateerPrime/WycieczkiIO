using System.ComponentModel.DataAnnotations;

namespace WycieczkiIO.Models
{
    public class Uczestnik
    {
        public int UczestnikId { get; set; }
        [MaxLength(30)]
        [Required]
        public string Imie { get; set; }
        [MaxLength(30)]
        [Required]
        public string Nazwisko { get; set; }
        [StringLength(11)]
        [Required]
        public string Pesel { get; set; }
        [MaxLength(30)]
        public string Telefon { get; set; }
        [MaxLength(60)]
        public string Email { get; set; }
        [Required]
        public int WycieczkaId { get; set; }
        public Wycieczka Wycieczka { get; set; }
    }
}