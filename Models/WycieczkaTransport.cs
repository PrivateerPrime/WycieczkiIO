namespace WycieczkiIO.Models
{
    public class WycieczkaTransport
    {
        public int TransportId { get; set; }
        public Transport Transport { get; set; }
        
        public int WycieczkaId { get; set; }
        public Wycieczka Wycieczka { get; set; }
    }
}