using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WycieczkiIO.Models
{
    public class Kraj
    {
        public int KrajId { get; set; }
        [Required]
        [MaxLength(60)]
        public string NazwaKraju { get; set; }
        public ICollection<Adres> Adresy { get; set; }
    }
}