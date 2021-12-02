using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CodingDays
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Database.DB>(opt => opt.UseMySql(GetConnectionString(), ServerVersion.Parse("10.3.0-mariadb")));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(routes => routes.MapControllerRoute("default", "api/register", new { action = "Register", controller = "Register" }));
        }

        private string GetConnectionString()
        {
            var result = "server=db;port=3306;database=CodingDays";
            
            // var result = _configuration.GetConnectionString("DefaultConnection");

            // get password from env
            var password = Environment.GetEnvironmentVariable("MYSQL_ROOT_PASSWORD");
            if (password is not null)
                result = $"{result};user=root;password={password}";

            return result;
        }
    }
}
