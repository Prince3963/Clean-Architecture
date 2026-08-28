using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Services.Implementation;
using MyApp.Application.Services.Interface;
using MyApp.Domain.Validator.EmployeeValidation;

namespace MyApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            return services;
        }
    }
}
