using System.ComponentModel.DataAnnotations;

namespace WycieczkiIO.Models
{
    public class Adres
    {
        public int AdresId { get; set; }
        [MaxLength(60)]
        public string Ulica { get; set; }
        [Required]   
        public int Numer { get; set; }
        [Required]
        [MaxLength(10)]
        public string KodPocztowy { get; set; }
        [Required]
        public int MiastoId { get; set; }
        public Miasto Miasto { get; set; }
        [Required]
        public int KrajId { get; set; }
        public Kraj Kraj { get; set; }
    }
}