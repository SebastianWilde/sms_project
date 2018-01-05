using Microsoft.EntityFrameworkCore;

namespace sms_project.Models
{
    public class sms_projectContext : DbContext
    {
        public sms_projectContext (DbContextOptions<sms_projectContext> options)
            : base(options)
        {
        }

        public DbSet<sms_project.Models.Destinatario> Movie { get; set; }
    }
}