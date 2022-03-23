using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodingDays.UserApi;
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
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(routes => routes.MapControllerRoute("default", "api/{controller}/{action}"));

        Setup(app, env);
    }

    private string GetConnectionString()
    {
        // get config from env
        string host = Environment.GetEnvironmentVariable("MYSQL_HOST")
            ?? throw new Exception("Missing MYSQL_HOST");
        string port = Environment.GetEnvironmentVariable("MYSQL_PORT")
            ?? throw new Exception("Missing MYSQL_PORT");
        string password = Environment.GetEnvironmentVariable("MYSQL_ROOT_PASSWORD")
            ?? throw new Exception("Missing MYSQL_ROOT_PASSWORD");

        return $"server={host};port={port};database=CodingDays;user=root;password={password}";
    }

    private void Setup(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // scoped
        using (IServiceScope serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope())
        {
            serviceScope.ServiceProvider.GetService<Database.DB>()!.Database.Migrate();
        }
    }
}

