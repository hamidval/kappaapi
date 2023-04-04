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
        public int? Id { get; set; }
        public int StudentId { get; set; }
        public decimal Hours { get; set; }

        public Subject SubjectId { get; set; }

        public YearGroup YearGroupId { get; set; }

        public decimal GroupFee { get; set; }

        public decimal SingleFee { get; set; }

        public decimal GroupPay { get; set; }

        public decimal SinglePay { get; set; }

        public LessonType LessonType { get; set; }

        public TakenLessonPaidStatus Status { get; set; }


    }


}
