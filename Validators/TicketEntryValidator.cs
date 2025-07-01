using FluentValidation;
using TicketBookingApp.Models;

namespace TicketBookingApp.Validators
{
    public class TicketEntryValidator : AbstractValidator<TicketEntry>
    {
        public TicketEntryValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
            RuleFor(x => x.TrainNo).NotEmpty().WithMessage("Train number is required.");
        }
    }
}
