using Microsoft.Extensions.Options;

namespace KappaApi
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) 
        {
            services.AddCors(options => 
            {
                options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());



                options.AddPolicy(name: "api",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000",
                                              "https://localhost:3000")
                          .AllowAnyOrigin()
                          .AllowAnyMethod()
                         .AllowAnyHeader();
                      });
            });

         


            
        }
    }
}
