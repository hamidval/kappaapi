using KappaApi.Enums;

namespace KappaApi.Queries.Dtos.LessonQuery
{
    public class LessonFormDto
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public YearGroup YearGroup { get; set; }
        public Subject Subject { get; set; }
        public string Day { get; set; }
        public decimal SingleFee { get; set; }
        public decimal SinglePay { get; set; }
        public decimal GroupFee { get; set; }
        public decimal GroupPay { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
