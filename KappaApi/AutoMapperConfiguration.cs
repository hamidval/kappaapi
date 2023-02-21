using AutoMapper;
using KappaApi.Models;
using KappaApi.Models.Api;
using KappaApi.Models.Dtos;
using System.Configuration;

namespace KappaApi
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Configure() 
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ApiProfile>();
            });

            return config.CreateMapper();
        }


        private class ApiProfile : Profile 
        {
            public ApiProfile() 
            {
                CreateMap<LessonApiModel, Lesson>()
                    .ForPath(x => x.Student.Id, o => o.MapFrom(m => m.StudentId))
                    .ForPath(x => x.Teacher.Id, o => o.MapFrom(m => m.TeacherId))
                    .ForMember(x => x.StartDate, o => o.MapFrom(m => m.StartDate))
                    .ForMember(x => x.EndDate, o => o.MapFrom(m => m.EndDate));

                CreateMap<Lesson, TakenLesson>()
                    .ForMember(x => x.TotalFee, m => m.Ignore())
                    .ForMember(x => x.TotalPay, m => m.Ignore());

                CreateMap<LessonDto, TakenLesson>()
                    .ForMember(x => x.TotalFee, m => m.Ignore())
                    .ForMember(x => x.TotalPay, m => m.Ignore())
                    .ForMember(x => x.Hours, m => m.Ignore())
                    .ForPath(x => x.LessonPrice.Subject, o => o.MapFrom(m => m.Subject))
                    .ForPath(x => x.LessonPrice.YearGroup, o => o.MapFrom(m => m.YearGroup))
                    .ForPath(x => x.LessonPrice.SingleFee, o => o.MapFrom(m => m.SingleFee))
                    .ForPath(x => x.LessonPrice.GroupFee, o => o.MapFrom(m => m.GroupFee))
                    .ForPath(x => x.LessonPrice.GroupPay, o => o.MapFrom(m => m.GroupPay))
                    .ForPath(x => x.LessonPrice.LessonType, o => o.MapFrom(m => m.LessonType))
                    .ForPath(x => x.Student.Id, o => o.MapFrom(m => m.StudentId))
                    .ForPath(x => x.Teacher.Id, o => o.MapFrom(m => m.TeacherId))
                    .ForPath(x => x.LessonDate, o => o.Ignore());

                CreateMap<TakenLessonModel, TakenLesson>()
                    .ForMember(x => x.TotalFee, m => m.Ignore())
                    .ForMember(x => x.TotalPay, m => m.Ignore())
                    .ForMember(x => x.Hours, o => o.MapFrom(m => m.Hours))
                    .ForPath(x => x.LessonPrice.Subject, o => o.MapFrom(m => m.Subject))
                    .ForPath(x => x.LessonPrice.YearGroup, o => o.MapFrom(m => m.YearGroup))
                    .ForPath(x => x.LessonPrice.SingleFee, o => o.MapFrom(m => m.SingleFee))
                    .ForPath(x => x.LessonPrice.SinglePay, o => o.MapFrom(m => m.SinglePay))
                    .ForPath(x => x.LessonPrice.GroupFee, o => o.MapFrom(m => m.GroupFee))
                    .ForPath(x => x.LessonPrice.GroupPay, o => o.MapFrom(m => m.GroupPay))
                    .ForPath(x => x.LessonPrice.LessonType, o => o.MapFrom(m => m.LessonType))
                    .ForPath(x => x.Student.Id, o => o.MapFrom(m => m.StudentId))
                    .ForPath(x => x.Teacher.Id, o => o.Ignore())
                    .ForPath(x => x.LessonDate, o => o.Ignore());

                CreateMap<TakenLessonDto, TakenLesson>()
                    .ForMember(x => x.Id, o => o.MapFrom(m => m._id))
                    .ForPath(x => x.LessonPrice.Subject, o => o.MapFrom(m => m.Subject))
                    .ForPath(x => x.LessonPrice.YearGroup, o => o.MapFrom(m => m.YearGroup))
                    .ForPath(x => x.LessonPrice.SingleFee, o => o.MapFrom(m => m.SingleFee))
                    .ForPath(x => x.LessonPrice.SinglePay, o => o.MapFrom(m => m.SinglePay))
                    .ForPath(x => x.LessonPrice.GroupFee, o => o.MapFrom(m => m.GroupFee))
                    .ForPath(x => x.LessonPrice.GroupPay, o => o.MapFrom(m => m.GroupPay))
                    .ForPath(x => x.LessonPrice.LessonType, o => o.MapFrom(m => m.LessonType))
                    .ForPath(x => x.Student.Id, o => o.MapFrom(m => m.StudentId))
                    .ForPath(x => x.Teacher.Id, o => o.MapFrom(m => m.TeacherId))
                    .ForPath(x => x.LessonDate, o => o.MapFrom(m => m.LessonDate))
                    .ForPath(x => x.StripeInvoiceId, o => o.Ignore());

                CreateMap<Parent, ParentDto>();                   


                CreateMap<ParentStudentLessonApiModel, Parent>()
                    .ForPath(x => x.Email, o => o.MapFrom(m => m.ParentEmail))
                    .ForPath(x => x.FirstName, o => o.MapFrom(m => m.ParentFirstName))
                    .ForPath(x => x.LastName, o => o.MapFrom(m => m.ParentLastName));

                CreateMap<ParentStudentLessonApiModel, Parent>()
                    .ForPath(x => x.Email, o => o.MapFrom(m => m.ParentEmail))
                    .ForPath(x => x.FirstName, o => o.MapFrom(m => m.ParentFirstName))
                    .ForPath(x => x.LastName, o => o.MapFrom(m => m.ParentLastName));

                CreateMap<StudentApiModel, Student>()
                    .ForMember(x => x.FirstName, o => o.MapFrom(m => m.FirstName))
                    .ForMember(x => x.LastName, o => o.MapFrom(m => m.LastName));






            }
        }
    }
}
