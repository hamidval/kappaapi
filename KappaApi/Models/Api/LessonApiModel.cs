using KappaApi.Domain;
using KappaApi.Enums;

namespace KappaApi.Models.Api
{
    public class LessonApiModel
    {
        public LessonApiModel() { }
        public virtual int StudentId { get; set; }
        public virtual int TeacherId { get; set; }
        public Subject Subject { get; set; }
        public YearGroup YearGroup { get; set; }
        public decimal SingleFee { get; set; }
        public decimal SinglePay { get; set; }
        public decimal GroupFee { get; set; }
        public decimal GroupPay { get; set; }
        public LessonType LessonType { get; set; }
        public decimal PayAdjustment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}
