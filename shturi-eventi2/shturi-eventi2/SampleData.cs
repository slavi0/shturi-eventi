using System.Linq;
using System.Numerics;
using shturi_eventi2.Models;
using static shturi_eventi2.Models.EventContext;

namespace shturi_eventi2
{
    public static class SampleData
    {
        public static void Initialize(EventContext context)
        {
            if (!context.Events.Any())
            {
                context.Events.AddRange(
                    new Event
                    {
                        Name = "Shtur event 1",
                        Description = "Purviqt shtur event v bazata danni",
                        Photo = File.ReadAllBytes("Snimka.jpg"),
                        Date = new DateTime(2023, 5, 15)
                    }
                );
                context.SaveChanges();
            }
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        Username = "admin",
                        Password = "12345",
                        FirstName = "admin",
                        LastName = "adminov",
                        Role = UserRole.Administrator
                    }
                );
                context.SaveChanges();
            }
            if (!context.Tickets.Any())
            {
                context.Tickets.AddRange(
                    new Ticket
                    {
                        UserId = 1,
                        EventId = 1
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
