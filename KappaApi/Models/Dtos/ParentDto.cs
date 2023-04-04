namespace KappaApi.Models.Dtos
{
    public class ParentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public  string? StripeCustomerId { get; set; }

        public string Status { get; set; }
    }
}
