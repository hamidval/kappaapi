using KappaApi.Models.Api;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Dtos.TakenLessonQuery;

namespace KappaApi.Queries.Contracts
{
    public interface ITakenLessonQuery
    {

        public IList<RegisterTakenLessonDto> GetTakenLessons(int teacherId, DateTime date);
        public IList<TakenLessonDto> GetTakenLessonDtos(int teacherId, DateTime date);

        public IList<TakenLessonDto> GetUninvoicedTakenLessons(int parentId);

        public IList<TakenLessonPanelDto> GetTakenLessonsBetweenDates(DateTime? startDate, DateTime? endDate, int? teacherId,
            int? parentId, int? studentId, int? invoiceId, string? stripeInvoiceId, string? stripeRefundId, int? pageNumber, int? pageSize);

        public IList<TakenLessonDto> GetTakenLessons(string? invoiceId, string? stripeInvoiceId, int? parentId);
        public TakenLessonDto GetTakenLessonById(int id);
        public IList<TakenLessonModelDto> GetTakenLessonsByStripeInvoiceId(string id);
        public int GetNumberOfPages(int? pageSize = 2);
        public int GetTakenLessonsBetweenDatesCount(DateTime? startDate, DateTime? endDate, int? teacherId, int? parentId, int? studentId, int? invoiceId, string? stripeInvoiceId, string? stripeRefundId);

        public int GetTakenLessonsBetweenDatesNumberOfPages(DateTime? startDate, 
            DateTime? endDate, int? teacherId, int? parentId, int? studentId, int? invoiceId,
            string? stripeInvoiceId, string? stripeRefundId, int? pageSize = 2);
    }
}
