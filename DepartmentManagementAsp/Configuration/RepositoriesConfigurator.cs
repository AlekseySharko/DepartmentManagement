using DepartmentManagementEfCore.Repositories;
using DepartmentManagementModels.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DepartmentManagementAsp.Configuration
{ 
    static class RepositoriesConfigurator
    {
        public static void AddEfRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, EfDepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EfEmployeeRepository>();
        }
    }
}
