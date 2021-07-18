using DepartmentManagementAsp.Configuration;
using DepartmentManagementEfCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
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
            //TODO - remove on production
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../ClientApp/dist";
            });
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
            else
            {
                app.UseExceptionHandler("/Error");
            }
            
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa => {
                //TODO - remove on production
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
