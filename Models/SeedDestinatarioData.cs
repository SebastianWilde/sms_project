using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
namespace sms_project.Models
{
    public static class SeedDestinatarioData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new sms_projectContext(
                serviceProvider.GetRequiredService<DbContextOptions<sms_projectContext>>()))
            {
                // Look for any movies.
                if (context.Movie.Any())
                {
                    return;   // DB has been seeded
                }

                string JsonPaht ="../sms_project/Data/DestinatarioData.json";
                Console.WriteLine(JsonPaht);
                string Json = System.IO.File.ReadAllText(JsonPaht);

                IEnumerable<Destinatario>  destinatarios = JsonConvert.DeserializeObject<IEnumerable<Destinatario>>(Json);
                context.AddRange(destinatarios);
                context.SaveChanges();
            }
        }
    }
}