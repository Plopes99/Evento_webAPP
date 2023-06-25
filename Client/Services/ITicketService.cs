using Events_WebAPP.Server;

namespace Events_WebAPP.Client.Services;

public interface ITicketService
{
    
    Task<IEnumerable<Ticket>?> All();
    Task<Ticket?> GetTicket(int id);
    Task<Ticket?> AddTicket(Ticket ticket);
    Task<bool> Update(Ticket ticket);
    Task<bool> Delete(int id);
    
}