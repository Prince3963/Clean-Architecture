using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.RepoInterface;
using MyApp.Infrastrcture.Data;
using MyApp.Infrastrcture.RepoImplementation;

namespace MyApp.Infrastcture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastrctureDI(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("Data Source=PRINCE\\SQLEXPRESS;Initial Catalog=cleanArchitecture;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True");
            });

            services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            services.AddScoped<IDepartmentRepo, DepartmentRepo>();

            return services;
        }
    }
}
