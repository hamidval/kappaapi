using KappaApi.Enums;

namespace KappaApi.Queries.Dtos.TakenLessonQuery
{
    public class RegisterTakenLessonDto
    {        
        public int Id { get; set;  }
        public decimal Hours { get; set; }
        public string Subject { get; set; }
        public decimal GroupFee { get; set; }
        public decimal SingleFee { get; set; }
        public decimal SinglePay { get; set; }
        public decimal GroupPay { get; set; }

        public int LessonType { get; set; }

        public string StudentName { get; set;}
        public int StudentId { get; set;}

        public string TakenLessonPaidStatus { get; set; }

        public TakenLessonPaidStatus? Status { get; set;  }

        public string StripeInvoiceId { get; set;  }
        public string StripeRefundId { get; set;  }
        public int InvoiceId { get; set;  }


    }
}
