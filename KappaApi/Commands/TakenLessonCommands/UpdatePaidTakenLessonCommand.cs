using Stripe;

namespace KappaApi.Commands.TakenLessonCommands
{
    public class UpdatePaidTakenLessonCommand
    {
        public Invoice Invoice { get; set; }
    }
}
