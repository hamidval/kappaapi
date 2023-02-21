namespace KappaApi.Commands.InvoiceCommands
{
    public class BuildInvoiceCommand
    {
        public BuildInvoiceCommand(List<int> parentIds) 
        {
            ParentIds = parentIds;
        }
        public List<int> ParentIds { get; set; }
    }
}
