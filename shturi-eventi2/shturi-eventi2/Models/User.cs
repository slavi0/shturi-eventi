namespace shturi_eventi2.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public virtual ICollection<Ticket>? Tickets { get; set; }
    }
    public enum UserRole
    {
        Administrator,
        OrdinaryUser
    }
}
