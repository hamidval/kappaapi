using KappaApi.Enums;
using System.Security.Cryptography;

namespace KappaApi.Queries.Dtos.LessonQuery
{
    public class LessonPanelLessonDto
    {
        public LessonPanelLessonDto() { }
            
        
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string TeacherName { get; set; }        
        public string Subject { get; set; }
        public string YearGroup { get; set; }
        public decimal SingleFee { get; set; }
        public decimal SinglePay { get; set; }
        public decimal GroupFee { get; set; }
        public decimal GroupPay { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Day { get; set; }

        
    }
}
