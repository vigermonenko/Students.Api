using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Students.Api.Configuration;
using Students.Api.Settings;
using Students.Core.Services;
using Students.Core.Services.Abstractions;
using Students.Infrastructure.Entities;


namespace Students.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;


        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Settings.Infrastructure>(_configuration.GetSection(nameof(Settings.Infrastructure)));
            services.Configure<AppSettings>(_configuration);

            services.AddControllers();

            services.AddSingleton<ICoreContextSettings, CoreContextSettings>();
            services.AddSingleton<IStudentsService, StudentsService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(e => e.MapControllers());
        }
    }
}