using DealerMarket.Core.Application.Interfaces.Services;
using DealerMarket.Core.Domain.Settings;
using DealerMarket.Infraestructure.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerMarket.Infraestructure.Shared
{
    public static class ServicesRegistration
    {
        public static void AddSharedInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailServices>();
        }
    }
}
