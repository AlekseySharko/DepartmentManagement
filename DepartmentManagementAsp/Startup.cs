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
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../ClientApp/dist/ClientApp";
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
            string strategy = Configuration
                .GetValue<string>("DevTools:ConnectionStrategy");
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                if (strategy == "managed")
                {
                    endpoints.MapFallback(HandleFallback);
                }
            });


            app.UseSpa(spa => {
                if (strategy == "proxy")
                {
                    spa.UseProxyToSpaDevelopmentServer("http://127.0.0.1:4200");
                }
                else if (strategy == "managed")
                {
                    // spa.Options.SourcePath = "../ClientApp";
                    //
                    // if (env.IsDevelopment())
                    // {
                    //     spa.UseAngularCliServer(npmScript: "start");
                    // }
                }
            });
        }

        private async Task HandleFallback(HttpContext context)
        {
            await Task.CompletedTask;
            var apiPathSegment = new PathString("/api");
            bool isApiRequest = context.Request.Path.StartsWithSegments(apiPathSegment);

            if (!isApiRequest)
            {
                context.Response.Redirect("index.html");
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
        }
    }
}