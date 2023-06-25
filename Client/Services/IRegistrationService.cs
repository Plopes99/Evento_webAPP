using Events_WebAPP.Server;

namespace Events_WebAPP.Client.Services;

public interface IRegistrationService
{
    Task<IEnumerable<Registration>?> All();
    Task<Registration?> GetRegistration(int id);
    Task<Registration?> AddRegistration(Registration regist);
    Task<bool> Update(Registration evento);
    Task<bool> Delete(int id);
}