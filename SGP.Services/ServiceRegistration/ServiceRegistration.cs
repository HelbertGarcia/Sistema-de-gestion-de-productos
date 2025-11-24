using Microsoft.Extensions.DependencyInjection;
using SGP.Services.Interfaces.Customer;
using SGP.Services.Services;

namespace SGP.Services.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Aquí se registran los servicios de la capa de servicios
            // Usamos Scoped porque utiliza el DbContext (que también es Scoped)
            services.AddScoped<ICustomerService, CustomerService>();

            // Futuro: services.AddScoped<IProductService, ProductService>();
            // Futuro: services.AddScoped<IInvoiceService, InvoiceService>();

            return services;
        }
    }
}
