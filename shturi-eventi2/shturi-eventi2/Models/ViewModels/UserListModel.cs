namespace shturi_eventi2.Models.ViewModels
{
    public class UserListModel
    {
        public List<User> Users;
        public UserListModel(List<User> users)
        {
            Users = users;
        }
    }
}
