using KappaApi.Enums;
using Microsoft.Identity.Client;

namespace KappaApi.Models.Api
{
    public class TakenLessonApiModel
    {
        public int TeacherId { get; set; }
        public List<TakenLessonModel>  Lessons { get; set; }
        public DateTime Date { get; set; }
    }


    public class TakenLessonModel 
    {
        public int StudentId { get; set; }
        public decimal Hours { get; set; }

        public Subject Subject { get; set; }

        public YearGroup YearGroup { get; set; }

        public decimal GroupFee { get; set; }

        public decimal SingleFee { get; set; }

        public decimal GroupPay { get; set; }

        public decimal SinglePay { get; set; }

        public LessonType LessonType { get; set; }


    }


}
