using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DepartmentsAndEmployeesEfCore;
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
            services.AddDbContext<DepartmentsAndEmployeesContext>(opts =>
            {
                opts.UseSqlServer(Configuration["ConnectionStrings:DepartmentsAndEmployeesConnection"]);
            });
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DepartmentsAndEmployeesDataSeeder.Seed(app.ApplicationServices
                    .CreateScope().ServiceProvider.GetRequiredService<DepartmentsAndEmployeesContext>());
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa => {
                string strategy = Configuration
                    .GetValue<string>("DevTools:ConnectionStrategy");
                if (strategy == "proxy")
                {
                    spa.UseProxyToSpaDevelopmentServer("http://127.0.0.1:4200");
                }
                else if (strategy == "managed")
                {
                    spa.Options.SourcePath = "../ClientApp";
                    spa.UseAngularCliServer("start");
                }
            });
        }
    }
}
