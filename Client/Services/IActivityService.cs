using Events_WebAPP.Server;

namespace Events_WebAPP.Client.Services;

public interface IActivityService
{
    Task<IEnumerable<Activity>?> All();
    Task<Activity?> GetActivity(int id);
    Task<Activity?> AddActivity(Activity activity);
    Task<bool> Update(Activity activity);
    Task<bool> Delete(int id);
}