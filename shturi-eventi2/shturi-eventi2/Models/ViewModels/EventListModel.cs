namespace shturi_eventi2.Models.ViewModels
{
    public class EventListModel
    {
        public List<Event> Events;
        public EventListModel(List<Event> events)
        {
            Events = events;
        }
    }
}
