using DealerMarket.Core.Application.Interfaces.Services;
using DealerMarket.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DealerMarket.Core.Application
{
    public static class ServicesRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            //Injecting Automapper / Configurating AutoMapper
            #region AutoMappers
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion

            //Dependency Injections
            #region Injections Services
            services.AddTransient<IAdsService, AdsServices>();
            services.AddTransient<ICategoryService, CategoryServices>();
            services.AddTransient<IUserService, UserService>();
            #endregion
        }
    }
}
