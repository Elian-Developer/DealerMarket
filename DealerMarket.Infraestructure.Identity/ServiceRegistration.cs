using DealerMarket.Core.Application.Interfaces.Services;
using DealerMarket.Infraestructure.Identity.Contexts;
using DealerMarket.Infraestructure.Identity.Entities;
using DealerMarket.Infraestructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace DealerMarket.Infraestructure.Identity
{
    public static class ServiceRegistration
    {
        public static void AddIdentityInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context Identity
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityDb"));
            }
            else
            {
                services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
            }
            #endregion

            //Adding Identity configurations

            #region Identity  
            services.AddIdentity<ApplicationUser, IdentityRole>()   // Indicando que voy a usar identity y especificandole las entidades que estamos utilizando, donde estamos guardando y tambien indicandole que maneje los tokens automaticamente.
                .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User";                      //Este codigo es para indicarle la ruta a donde redireccionar
                                                                  //cuando no hay permisos.
                options.AccessDeniedPath = "/User/AccessDenied";
            });

            services.AddAuthentication();  // Indicarle al services que maneje las autenticaciones
            #endregion

            #region Services
            services.AddTransient<IAccountService, AccountService>();
            #endregion
        }
    }
}
