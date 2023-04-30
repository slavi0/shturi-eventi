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
                    return RedirectToAction("MainPageUser", user.Id);
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
            return View();
        }
        [HttpGet]
        public IActionResult MainPageUser() 
        { 
            return View();
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
    }
}
