namespace WycieczkiIO.Models
{
    public class WycieczkaAtrakcja
    {
        public int AtrakcjaId { get; set; }
        public Atrakcja Atrakcja { get; set; }
        
        public int WycieczkaId { get; set; }
        public Wycieczka Wycieczka { get; set; }
        
    }
}