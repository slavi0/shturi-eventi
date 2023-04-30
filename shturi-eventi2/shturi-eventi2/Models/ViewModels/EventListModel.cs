namespace shturi_eventi2.Models.ViewModels
{
    public class EventListModel
    {
        public List<Event> Events;
        public int UserId;
        public EventListModel(List<Event> events)
        {
            Events = events;
        }
        public EventListModel(List<Event> events, int id)
        {
            Events = events;
            UserId = id;
        }
    }
}
