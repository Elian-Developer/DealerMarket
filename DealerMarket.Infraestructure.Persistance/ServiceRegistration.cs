using DealerMarket.Core.Application.Interfaces.Repositories;
using DealerMarket.Infraestructure.Persistance.Contexts;
using DealerMarket.Infraestructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerMarket.Infraestructure.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }
            #endregion

            //Dependency Injections
            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IAdsRepository, AdsRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            #endregion
        }

    }
}
