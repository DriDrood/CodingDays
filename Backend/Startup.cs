using System;
using CodingDays.Models.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
            JwtHolder jwtHolder = RegisterJwt(services);
            services.AddSingleton<JwtHolder>(sp => jwtHolder);
            services.AddSingleton<SecretHolder>(sp => GetSecretHolder());

            services.AddSingleton<Utils.JwtHandler>();
            services.AddScoped<Models.TeamHolder>();

            services.AddDbContext<Database.DB>(opt => opt.UseMySql(GetConnectionString(), ServerVersion.Parse("10.3.0-mariadb")));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<Exceptions.ExceptionMiddleware>();
            app.UseMiddleware<Utils.TeamMiddleware>();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(routes => routes.MapControllerRoute("default", "api/{controller}/{action}"));

            Setup(app, env);
        }

        private string GetConnectionString()
        {
            // get config from env
            var host = Environment.GetEnvironmentVariable("MYSQL_HOST");
            var password = Environment.GetEnvironmentVariable("MYSQL_ROOT_PASSWORD");

            return $"server={host};port=3306;database=CodingDays;user=root;password={password}";
        }
        private JwtHolder RegisterJwt(IServiceCollection services)
        {
            string key = Environment.GetEnvironmentVariable("JWT_PRIVATE_KEY")
                ?? throw new Exception("Missing JWT private key");
            JwtHolder jwtHolder = new JwtHolder(key);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => opt.TokenValidationParameters = Utils.JwtHandler.GetTokenParams(jwtHolder));

            return jwtHolder;
        }
        private SecretHolder GetSecretHolder()
        {
            string secret = Environment.GetEnvironmentVariable("SECRET")
                ?? throw new Exception("Missing secret");

            return new SecretHolder(secret);
        }

        private void Setup(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // scoped
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                serviceScope.ServiceProvider.GetService<Database.DB>()!.Database.Migrate();
            }
        }
    }
}
