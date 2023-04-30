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
    }
}
