using KappaApi.Domain;
using KappaApi.Enums;
using System.Security.Cryptography.X509Certificates;

namespace KappaApi.Models
{
    public class Lesson
    {
        public Lesson()
        {
            Teacher = new Teacher();
            Student = new Student();
        }

        public virtual int Id { get; set; }
        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual DateTime EndDate {get; set;}

        public virtual LessonPrice LessonPrice {get; set;}

        public virtual int Day { get; set; }

        public static Student CreateStudent(int id, string firstName, string lastName) 
        {
            var student = new Student() 
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            };
            return student;
        }

        public static Teacher CreateTeacher(int id, string firstName, string lastName, string email)
        {
            var teacher = new Teacher()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
            return teacher;
        }

        public static LessonPrice CreateLessonPrice(Subject subject, YearGroup yearGroup, LessonType lessonType,
            decimal singleFee, decimal groupFee,
            decimal singlePay, decimal groupPay)
        {
            var price = new LessonPrice(subject, yearGroup, lessonType,
            singleFee, groupFee,
            singlePay, groupPay);
            
                
            return price;
        }
    }
}
