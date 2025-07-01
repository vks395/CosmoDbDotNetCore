using TicketBookingApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketBookingApp.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketEntry>> GetAllAsync();
        Task<TicketEntry> GetByIdAsync(string id);
        Task AddAsync(TicketEntry ticket);
        Task UpdateAsync(string id, TicketEntry ticket);
        Task DeleteAsync(string id);
    }
}
