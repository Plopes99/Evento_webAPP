using Events_WebAPP.Server;

namespace Events_WebAPP.Client.Services;

public interface IUserService
{
    Task<IEnumerable<User>?> All();
    Task<User?> GetUser(int id);
    Task<User?> AddUser(User user);
    Task<bool> Update(User user);
    Task<bool> Delete(int id);


}