using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WycieczkiIO.Models
{
    public class Miasto
    {
        public int MiastoId { get; set; }
        [Required]
        [MaxLength(60)]
        public string NazwaMiasta { get; set; }
        public ICollection<Adres> Adresy { get; set; }
    }
}