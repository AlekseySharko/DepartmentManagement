using System.Threading.Tasks;
using DepartmentManagementAsp.Configuration;
using DepartmentManagementEfCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DepartmentManagementAsp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DepartmentManagementContext>(opts =>
            {
                opts.UseSqlServer(Configuration["ConnectionStrings:DepartmentsAndEmployeesConnection"]);
            });
            services.AddEfRepositories();
            services.AddDefaultValidators();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DepartmentManagementDataSeeder.Seed(app.ApplicationServices
                    .CreateScope().ServiceProvider.GetRequiredService<DepartmentManagementContext>());
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseSpa(spa => {
                if (Configuration.GetValue<string>("DevTools:ConnectionStrategy") == "proxy" &&
                    env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://127.0.0.1:4200");
                }
            });
        }
    }
}