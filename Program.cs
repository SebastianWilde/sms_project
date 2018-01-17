using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using sms_project.Models;
namespace sms_project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {

                var services = scope.ServiceProvider;
                // try
                // {
                //     System.Diagnostics.Trace.WriteLine("Iniciando seed");
                //     SeedDestinatarioData.Initialize(services);
                //     System.Diagnostics.Trace.WriteLine("Finalizando seed");
                // }
                // catch (Exception ex)
                // {
                //     var logger = services.GetRequiredService<ILogger<Program>>();
                //     logger.LogError(ex, "An error occurred seeding the DB.");
                //     System.Diagnostics.Trace.TraceError("No se pudo subir a la basde de datos");
                // }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
