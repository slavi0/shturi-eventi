namespace shturi_eventi2.Models.ViewModels
{
    public class TicketListModel
    {
        public List<string[]> ticketInfo;
        public TicketListModel(List<Ticket> tickets, Business business) 
        {
            ticketInfo = new List<string[]>();
            foreach(Ticket ticket in tickets)
            {
                string[] element = new string[3];
                element[0] = business.GetEventNameByTicket(ticket);
                element[1] = business.GetUserNameByTicket(ticket);
                element[2] = ticket.Id.ToString();
                ticketInfo.Add(element);
            }
        }
    }
}
