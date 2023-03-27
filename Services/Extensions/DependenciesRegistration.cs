using Microsoft.Extensions.DependencyInjection;
using Task.Repository;
using Web.Services;

namespace Task.Services
{
    public static class DependenciesRegistration
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddServices();
            RepositoriesRegistration.AddRepositories(services);
            return services;
        }

        #region Private methods

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            return services;
        }

        #endregion
    }
}