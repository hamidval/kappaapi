using KappaApi.Enums;

namespace KappaApi.Models.Dtos
{
    public class LessonDto
    {
        public LessonDto(int id, Subject subject, string teacher, DateTime startDate, DateTime endDate,
            int day, string dayText, 
            decimal singleFee,
            decimal singlePay,
            decimal groupFee,
            decimal groupPay,
            LessonType lessontype,
            int teacherId,
            YearGroup yearGroup
            ) 
        {
            Id = id;
            Subject = subject;
            SingleFee = singleFee;
            SinglePay = singlePay;
            GroupFee = groupFee;
            GroupPay = groupPay;          
            Teacher = teacher;
            StartDate = startDate;
            EndDate = endDate;
            Day = day;
            DayText = dayText;
            LessonType = lessontype;
            TeacherId = teacherId;
            YearGroup = yearGroup;
        }
        public int Id { get; set; }
        public int Day { get; set; }
        public string DayText { get; set; }
        public string SubjectText { get; set; }
        public Subject Subject { get; set; }  
        public decimal SingleFee { get; set; }
        public decimal SinglePay { get; set; }
        public decimal GroupFee { get; set; }
        public decimal GroupPay { get; set; }

        public LessonType LessonType { get; set; }

        public int TeacherId { get; set; }

        public string Teacher { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int StudentId { get; set; }

        public string StudentFirstName { get; set; }

        public YearGroup YearGroup { get; set; }

    }
}
