using KappaApi.Enums;

namespace KappaApi.Models.Dtos
{
    public class TakenLessonDto
    {
        public TakenLessonDto(int id,
            Subject subject,
            string teacher,        
            decimal singleFee,
            decimal singlePay,
            decimal groupFee,
            decimal groupPay,
            LessonType lessontype,
            int teacherId,
            YearGroup yearGroup,
            decimal totalFee,
            decimal totalPay,
            DateTime lessonDate
            ) 
        {
            _id = id;
            Subject = subject;
            SingleFee = singleFee;
            SinglePay = singlePay;
            GroupFee = groupFee;
            GroupPay = groupPay;          
            Teacher = teacher;         
            LessonType = lessontype;
            TeacherId = teacherId;
            YearGroup = yearGroup;
            TotalFee = totalFee;
            TotalPay = totalPay;
            LessonDate = lessonDate;
        }
        public int _id { get; set; }
        public Subject Subject { get; set; }  
        public decimal SingleFee { get; set; }
        public decimal SinglePay { get; set; }
        public decimal GroupFee { get; set; }
        public decimal GroupPay { get; set; }

        public LessonType LessonType { get; set; }

        public int TeacherId { get; set; }

        public string Teacher { get; set; }

        public int StudentId { get; set; }
        public string? StudentFirstName { get; set; }
        public YearGroup YearGroup { get; set; }
        public decimal TotalFee { get; set; }
        public decimal TotalPay { get; set; }
        public decimal Hours { get; set; }
        public DateTime LessonDate { get; set; }
        public string? StripeInvoiceId { get; set; }
        public string? StripeRefundId { get; set; }
        public string? TakenLessonPaidStatus { get; set; }
        public TakenLessonPaidStatus? Status { get; set; }
        public int InvoiceId { get; set; }

        public string SubjectText { get; set; }

    }
}
