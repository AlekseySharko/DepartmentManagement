using DepartmentManagementModels.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace DepartmentManagementAsp.Configuration
{
    static class ValidatorsConfigurator
    {
        public static void AddDefaultValidators(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentValidator, DepartmentValidator>();
        }
    }
}
