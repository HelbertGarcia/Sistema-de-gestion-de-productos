using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGP.Persistence.Context;

namespace SGP.Persistence.PersistenceServiceRegistration
{
    public static class DependencyInjection
    {
        //Registro el servicio de persistencia de forma que lo pueda inyectar en el proyecto principal
        //lo agrego al contenedor de servicios IServiceCollection
        public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //Configuro el DbContext con la cadena de conexion
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
