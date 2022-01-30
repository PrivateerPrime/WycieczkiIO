using System.ComponentModel.DataAnnotations.Schema;

namespace WycieczkiIO.Models
{
    public enum Status { Zaplacona, Niezaplacona }

    public class Platnosc
    {
        public int PlatnoscId { get; set; }

        public double Kwota { get; set; }
        
        public double Rabat { get; set; }
        public Status Status { get; set; }
    }
}