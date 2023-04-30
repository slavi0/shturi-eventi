using System.Net.Sockets;

namespace shturi_eventi2.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<Ticket>? Tickets { get; set; }
    }
}
