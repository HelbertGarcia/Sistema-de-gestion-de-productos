using SGP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SGP.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Esta linea aplica todas las configuraciones de entidades externas en el ensamblado actual
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public DbSet<Customer> Customers {get; set;}
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices {get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
