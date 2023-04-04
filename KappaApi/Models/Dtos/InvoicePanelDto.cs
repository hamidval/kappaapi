namespace KappaApi.Models.Dtos
{
    public class InvoicePanelDto
    {
        public InvoicePanelDto() 
        {
            Invoices = new List<InvoiceDto>();
        }

        public bool HasPrev { get; set; }
        public bool HasNext { get; set; }

        public List<InvoiceDto> Invoices { get; set; }


    }
}
