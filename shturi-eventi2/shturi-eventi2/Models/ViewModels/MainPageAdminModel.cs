namespace shturi_eventi2.Models.ViewModels
{
    public class MainPageAdminModel
    {
        public int eventCount;
        public int UserCount;
        public int TicketCount;
        public MainPageAdminModel(Business business) 
        { 
            eventCount = business.GetEventCount();
            UserCount = business.GetUserCount();
            TicketCount = business.GetTicketCount();
        }
    }
}
