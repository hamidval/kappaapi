using KappaApi.Enums;

namespace KappaApi.Domain
{
    public class LessonPrice
    {
        public LessonPrice(Subject subject, YearGroup yearGroup, LessonType lessonType,
            decimal singleFee, decimal groupFee,
            decimal singlePay, decimal groupPay)
        {
            Subject = subject;
            YearGroup = yearGroup;
            SingleFee = singleFee;
            GroupFee  = groupFee;  
            SinglePay = singlePay;
            GroupPay = groupPay;
    
        }

        public LessonPrice() { }

        public Subject Subject { get; set; }

        public YearGroup YearGroup { get; set; }

        public decimal GroupFee { get; set; }
       
        public decimal SingleFee { get; set; }

        public decimal GroupPay { get; set; }

        public decimal SinglePay { get; set; }

        public LessonType LessonType { get; set; }
        
    }
    
}
