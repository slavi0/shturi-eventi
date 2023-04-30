using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using shturi_eventi2.Models;
using shturi_eventi2.Models.ViewModels;

namespace shturi_eventi2.Controllers
{
    public class HomeController : Controller
    {
        EventContext db;
        Business business;
        public HomeController(EventContext context)
        {
            db = context;
            business = new Business(db);
        }

        [HttpGet]
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            user.Role = UserRole.OrdinaryUser;
            business.RegisterUser(user);
            user = business.GetUserByUsername(user.Username);
            return RedirectToAction("MainPageUser", user.Id);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (business.LogIn(username, password))
            {
                User user = business.GetUserByUsername(username);
                if (user.Role == UserRole.Administrator)
                    return RedirectToAction("MainPageAdmin", user.Id);
                else
                    return RedirectToAction("MainPageUser", new { Id = user.Id });
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult MainPageAdmin()
        {
            MainPageAdminModel model = new MainPageAdminModel(business);
            return View(model);
        }
        [HttpGet]
        public IActionResult MainPageUser(int Id) 
        { 
            User user = business.GetUserById(Id);
            return View(user);
        }
        [HttpGet]
        public IActionResult EventList()
        {
            EventListModel eventList = new EventListModel(business.GetAllEvents());
            return View(eventList);
        }
        [HttpGet]
        public IActionResult UserList()
        {
            UserListModel userList = new UserListModel(business.GetAllUsers());
            return View(userList);
        }

        [HttpGet]
        public IActionResult EventInfo(int eventId)
        {
            Event event1 = business.GetEventById(eventId);
            return View(event1);
        }
        [HttpPost]
        public IActionResult EventInfo(Event event1)
        {
            business.UpdateEvent(event1);
            return RedirectToAction("MainPageAdmin", "Home");
        }
        [HttpGet]
        public IActionResult EventInfoSaved(int eventId)
        {
            business.DeleteEventById(eventId);
            return RedirectToAction("MainPageAdmin", "Home");
        }
        [HttpGet]
        public IActionResult CreateEvent()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateEvent(Event event1)
        {
            business.AddEvent(event1);
            return RedirectToAction("MainPageAdmin", "Home");
        }
        [HttpGet]
        public IActionResult UserInfo(int userId)
        {
            User user = business.GetUserById(userId);
            return View(user);
        }
        [HttpPost]
        public IActionResult UserInfo(string username, string firstname, string lastname, int id)
        {
            User user = new User();
            user.Username = username;
            user.FirstName = firstname;
            user.LastName = lastname;
            user.Id = id;
            business.UpdateUser(user);
            return RedirectToAction("MainPageAdmin");
        }
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(string Username, string Password, string FirstName, string LastName)
        {
            User user = new User();
            user.Username = Username;
            user.Password = Password;
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Role = UserRole.OrdinaryUser;
            business.AddUser(user);
            return RedirectToAction("MainPageAdmin");
        }
        [HttpGet]
        public IActionResult UserInfoSaved(int userId)
        {
            business.RemoveUserById(userId);
            return View();
        }
        [HttpGet]
        public IActionResult UserEventList(int userId) 
        {
            EventListModel eventList = new EventListModel(business.GetAllEvents(), userId);
            return View(eventList);
        }
        [HttpGet]
        public IActionResult GetTicket(int eventId, int userId)
        {
            Ticket ticket = new Ticket();
            ticket.EventId = eventId;
            ticket.UserId = userId;
            business.AddTicket(ticket);
            User user = business.GetUserById(userId);
            return View(user);
        }
        [HttpGet]
        public IActionResult TicketList(int userId)
        {
            EventListModel eventList = new EventListModel(business.GetEventsForUser(userId), userId);
            return View(eventList);
        }
        [HttpGet]
        public IActionResult RemoveTicket(int eventId, int userId)
        {
            business.RemoveTicketByTwoId(eventId, userId);
            User user = business.GetUserById(userId);
            return View(user);
        }
        [HttpGet]
        public IActionResult TicketListAdmin()
        {
            TicketListModel ticketList = new TicketListModel(business.GetAllTickets(), business);
            return View(ticketList);
        }
        [HttpPost]
        public IActionResult RemoveTicket(int ticketId)
        {
            Ticket ticket = business.GetTicketById(ticketId);
            return RedirectToAction("RemoveTicketAdmin", new {eventId = ticket.EventId, userId = ticket.UserId});
        }
        [HttpGet]
        public IActionResult RemoveTicketAdmin(int eventId, int userId)
        {
            business.RemoveTicketByTwoId(eventId, userId);
            return View();
        }
    }
}
