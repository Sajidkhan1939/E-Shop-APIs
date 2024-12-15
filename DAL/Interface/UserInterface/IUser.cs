using JwtImplementation.BLL.Model.UserModel;

namespace DAL.Interface.UserInterface
{
    public interface IUser
    {
        Task<User> RegisterUser(User user);
        Task<User> GetUserByEmail(string email);
        Task<List<User>> GetAllUser();
        Task<string> Login(string email, string password);
    }
}
