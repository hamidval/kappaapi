using KappaApi.Domain;
using KappaApi.Enums;

namespace KappaApi.Models
{
    public class TakenLesson
    {

        public TakenLesson() 
        {
            Student = new Student();
            Teacher = new Teacher();
        }
     
        public virtual int Id { get; set; }
        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual LessonPrice LessonPrice { get; set; }
        public virtual decimal Hours {get; set;}
        public virtual decimal TotalPay { get; set;}
        public virtual decimal TotalFee { get; set;}
        public virtual DateTime LessonDate { get; set;}
        public virtual string? StripeInvoiceId { get; set; }
        public virtual string? StripeRefundId { get; set; }
        public virtual int? InvoiceId { get;set; }
        public virtual TakenLessonPaidStatus? TakenLessonPaidStatus { get; set; }
        

    }
}
