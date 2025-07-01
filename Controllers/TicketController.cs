using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketBookingApp.Models;
using TicketBookingApp.Interfaces;

namespace TicketBookingApp.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _service;

        public TicketController(ITicketService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _service.GetAllAsync();
            return View(tickets);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(TicketEntry ticket)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(ticket);
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var ticket = await _service.GetByIdAsync(id);
            return View(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, TicketEntry ticket)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(id, ticket);
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var ticket = await _service.GetByIdAsync(id);
            return View(ticket);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
