using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Domain;
using MyApp.Infrastcture;

namespace MyApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAPIDI(this IServiceCollection services)
        {
            services.AddInfrastrctureDI()
            .AddApplicationDI()
            .AddDomainDI();
            return services;
        }
    }
}
