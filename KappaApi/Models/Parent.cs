using KappaApi.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace KappaApi.Models
{
    public class Parent
    {
        public Parent(string? firstName, string? lastName, string? email, string? stripeCustomerId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            StripeCustomerId = stripeCustomerId;
        }

        public Parent() { }

       
        public virtual int Id { get; set; }
        public virtual string? FirstName { get; set; }
        public virtual string? LastName { get; set; }
        public virtual string? Email { get; set; }
        public virtual ParentStatus Status { get; set; }
        public virtual string? StripeCustomerId { get; set; }
    }
}
