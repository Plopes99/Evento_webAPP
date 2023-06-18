using Events_WebAPP.Server;

namespace Events_WebAPP.Client.Services;

public interface IEventService
{
    Task<IEnumerable<Event>?> All();
    Task<Event?> GetEvent(int id);
    Task<Event?> AddEvent(Event evento);
    Task<bool> Update(Event evento);
    Task<bool> Delete(int id);


}