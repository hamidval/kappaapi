using AutoMapper;
using CommonServiceLocator;
using KappaApi;
using KappaApi.Commands;
using KappaApi.Commands.LessonCommands;
using KappaApi.Commands.ParentCommands;
using KappaApi.Commands.StudentCommands;
using KappaApi.Commands.TakenLessonCommands;
using KappaApi.Commands.TeacherCommands;
using KappaApi.Controllers;
using KappaApi.NHibernateMappings;
using KappaApi.Queries;
using KappaApi.Queries.Contracts;
using KappaApi.Services.StripeService;
using Microsoft.EntityFrameworkCore;
using NHibernate;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System;
using System.ComponentModel;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using KappaApi.Models;
using Hangfire;
using Hangfire.SqlServer;
using KappaApi.Commands.InvoiceCommands;
using Stripe;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

//cors
builder.Services.ConfigureCors();


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc()
        .AddSessionStateTempDataProvider();
builder.Services.AddSession();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<KappaDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddSingleton<ILessonQuery, LessonQuery>();
builder.Services.AddSingleton<ITakenLessonQuery, TakenLessonQuery>();
builder.Services.AddSingleton<ITeacherQuery, TeacherQuery>();
builder.Services.AddSingleton<IParentQuery, ParentQuery>();
builder.Services.AddSingleton<IInvoiceQuery, InvoiceQuery>();
builder.Services.AddSingleton<IStudentQuery, StudentQuery>();
builder.Services.AddSingleton<IStripeService, StripeService>();
var connectionString = configuration.GetConnectionString("DefaultConnection") != null? configuration.GetConnectionString("DefaultConnection") : "";
var repo = NHibernateInitializer.GetSessionFactory("command", connectionString);



//CommandHandlerPattern
var container = new SimpleInjector.Container();

container.Register(typeof(ICommandHandler<>), new[]
         {
                typeof(CreateParentCommandHandler),
                typeof(CreateStudentCommandHandler),
                typeof(CreateTeacherCommandHandler),
                typeof(CreateLessonCommandHandler),
                typeof(CreateTakenLessonCommandHandler),
                typeof(CreateParentStudentLessonCommandHandler),
                typeof(BuildInvoiceCommandHandler)

            });


IMapper mapper = AutoMapperConfiguration.Configure();
container.Register(() => repo, Lifestyle.Singleton);
container.Register(() => mapper, Lifestyle.Singleton);
container.Register<ILessonQuery, LessonQuery>(Lifestyle.Singleton);
container.Register<IParentQuery, ParentQuery>(Lifestyle.Singleton);
container.Register<IInvoiceQuery, InvoiceQuery>(Lifestyle.Singleton);
container.Register<ITakenLessonQuery, TakenLessonQuery>(Lifestyle.Singleton);
container.Register<IStripeService, StripeService>(Lifestyle.Singleton);

builder.Services.AddSingleton(mapper);

ServiceLocator.SetLocatorProvider(() => new SimpleInjectorServiceLocator(container) );
//builder.Services.AddScoped<ICommandHandler<CreateParentCommand>, CreateParentCommandHandler>();
builder.Services.AddTransient<ICommandBus, CommandBus>();



//stripe

StripeConfiguration.ApiKey = builder.Configuration["Stripe:ApiKey"];



///// Hangfire
builder.Services.AddHangfire(configuration => 
    configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    }));

builder.Services.AddHangfireServer();
var hangfireJobs = new HangfireJobs();
hangfireJobs.InitializeJobs();


// NHibernate



//auth
string domain = builder.Configuration["Auth0:Domain"];
builder.Services
    .AddAuthentication(options =>    {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.Authority = domain;
        options.Audience = builder.Configuration["Auth0:Audience"];
        // If the access token does not have a `sub` claim, `User.Identity.Name` will be `null`. Map it to a different claim by setting the NameClaimType below.
        //options.TokenValidationParameters = new TokenValidationParameters
        //{
        //    NameClaimType = ClaimTypes.NameIdentifier
        //};
    });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ReadMessages", policy => policy.RequireClaim("permissions", "read:messages"));
});

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

//
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHangfireDashboard();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("api");
app.MapControllers();
app.UseSession();
//app.UseRouting();

app.Run();