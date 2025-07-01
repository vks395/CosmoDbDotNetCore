using System;
using System.ComponentModel.DataAnnotations;

namespace TicketBookingApp.Models
{
    public class TicketEntry
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string TicketNo { get; set; } = new Random().Next(100000000, 999999999).ToString();
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;

        [Required]
        public string Username { get; set; }

        public string Address { get; set; }
        public string State { get; set; }

        [Required]
        public string TrainNo { get; set; }
    }
}
