using shturi_eventi2.Models;

namespace shturi_eventi2
{
    public class Business
    {
        private EventContext db;
        public Business(EventContext database) 
        { 
            db = database;
        }
        public bool LogIn(string username, string password)
        {
            bool correctinfo = false;
            User? user = db.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return false;
            }
            else if(user.Password == password)
            {
                return true;
            }
            return false;
        }
        public User GetUserByUsername(string username)
        {
            User user = db.Users.FirstOrDefault(u =>u.Username == username);
            return user;
        }
        public void RegisterUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }
        public List<Event> GetAllEvents()
        {
            List<Event> events = db.Events.ToList();
            return events;
        }
        public List<User> GetAllUsers()
        {
            List<User> users = db.Users.Where(u => u.Role == UserRole.OrdinaryUser).ToList();
            return users;
        }
        public Event GetEventById(int id)
        {
            return db.Events.First(e => e.Id == id);
        }
        public void UpdateEvent(Event event1)
        {
            Event oldEvent = db.Events.First(e => e.Id == event1.Id);
            oldEvent.Name = event1.Name;
            oldEvent.Description = event1.Description;
            oldEvent.Date = event1.Date;
            if(event1.Photo != null)
            {
                oldEvent.Photo = event1.Photo;
            }
            if(oldEvent.Photo != null)
            {
                oldEvent.Photo = File.ReadAllBytes("Snimka.jpg");
            }
            db.Events.Update(oldEvent);
            db.SaveChanges();
        }
        public void DeleteEventById(int id)
        {
            db.Events.Remove(GetEventById(id));
            db.SaveChanges();
        }
        public void AddEvent(Event event1)
        {
            if(event1.Photo== null)
            {
                event1.Photo = File.ReadAllBytes("Snimka.jpg");
            }
            db.Events.Add(event1);
            db.SaveChanges();
        }
        public User GetUserById(int id)
        {
            User user = db.Users.First(u => u.Id == id);
            return user;
        }
        public void UpdateUser(User user)
        {
            User oldUser = db.Users.First(u => u.Id == user.Id);
            oldUser.Username = user.Username;
            oldUser.FirstName= user.FirstName;
            oldUser.LastName= user.LastName;
            db.SaveChanges();
        }
        public void AddUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }
        public void RemoveUserById(int id)
        {
            db.Users.Remove(GetUserById(id));
            db.SaveChanges();
        }
        public void AddTicket(Ticket ticket)
        {
            db.Tickets.Add(ticket);
            db.SaveChanges();
        }
        public List<Ticket> GetTicketsByUserId(int userId)
        {
            List<Ticket> tickets = db.Tickets.Where(t => t.UserId == userId).ToList();
            return tickets;
        }
        public List<Event> GetEventsForUser(int userId)
        {
            List<Ticket> tickets = GetTicketsByUserId(userId);
            List<Event> events = new List<Event>();
            foreach (Ticket ticket in tickets)
            {
                events = events.Concat(db.Events.Where(e => e.Id == ticket.EventId).ToList()).ToList();
            }
            return events;
        }
        public void RemoveTicketByTwoId(int eventId, int userId)
        {
            Ticket ticket = db.Tickets.First(t => t.EventId == eventId && t.UserId == userId);
            db.Remove(ticket);
            db.SaveChanges();
        }
        public string GetEventNameByTicket(Ticket ticket)
        {
            Event event1 = db.Events.First(e => e.Id == ticket.EventId);
            return event1.Name;
        }
        public string GetUserNameByTicket(Ticket ticket)
        {
            User user = db.Users.First(u => u.Id == ticket.UserId);
            return user.Username;
        }
        public List<Ticket> GetAllTickets()
        {
            List<Ticket> tickets = db.Tickets.ToList();
            return tickets;
        }
        public Ticket GetTicketById(int ticketId)
        {
            Ticket ticket = db.Tickets.FirstOrDefault(t => t.Id == ticketId);
            return ticket;
        }
        public int GetEventCount()
        {
            return db.Events.Count();
        }
        public int GetUserCount()
        {
            return db.Users.Count();
        }
        public int GetTicketCount()
        {
            return db.Tickets.Count();
        }
    }
}
