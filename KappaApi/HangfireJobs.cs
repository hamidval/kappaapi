using Dapper;
using Hangfire;
using Hangfire.SqlServer;
using KappaApi.Commands;
using KappaApi.Commands.InvoiceCommands;
using KappaApi.Models;
using KappaApi.Models.Dtos;
using KappaApi.Queries;
using KappaApi.Services.StripeService;
using Microsoft.Data.SqlClient;

namespace KappaApi
{
    public class HangfireJobs
    {
        private readonly ParentQuery _parentQuery = new ParentQuery();
        private readonly CommandBus _commandBus = new CommandBus();
 
        public void InitializeJobs() 
        {            
            var sqlStorage = new SqlServerStorage(DbConnectionFactory.ConnectionString);
            var options = new BackgroundJobServerOptions
            {
                ServerName = "Test Server",
            };
            JobStorage.Current = sqlStorage;

            StartCreateInvoicesJob();
        }

        public void StartCreateInvoicesJob() 
        {
            RecurringJob.AddOrUpdate("CreateInvoices", () => CreateInvoices(), Cron.Minutely);
        }

        public void CreateInvoices() 
        {            
            Console.WriteLine("createing invoice");
            List<int> parentIds = _parentQuery.GetParentsFromTakenLessons();

            var command = new BuildInvoiceCommand(parentIds);
            _commandBus.SendAsync(command);

           

        }
    }
}
