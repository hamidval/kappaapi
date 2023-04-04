namespace KappaApi.Queries.Dtos.LessonQuery
{
    public class RegisterLessonDto
    {
        public string Subject { get; set; }
        public string StudentName { get; set; }

        public int StudentId { get; set;}
        public decimal SingleFee { get; set; }
        public decimal SinglePay { get; set; }
        public decimal GroupFee { get; set; }
        public decimal GroupPay { get; set; }

        public int LessonId { get; set; }

        public int YearGroupId { get; set; }
        public int SubjectId { get; set; }



    }
}
