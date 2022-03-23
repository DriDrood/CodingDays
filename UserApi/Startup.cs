using System;
using CodingDays.UserApi.Models.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CodingDays.UserApi;
public static class Startup
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddDbContext<Database.DB>(opt => opt.UseMySql(GetConnectionString(), ServerVersion.Parse("10.3.0-mariadb")))
            .AddJwt()
            .AddSecret()

            .AddSingleton<Utils.JwtHandler>()
            .AddScoped<Models.AuthHolder>()

            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddControllers();

        return builder;
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public static WebApplication Configure(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseMiddleware<Exceptions.ExceptionMiddleware>();
        app.UseMiddleware<Utils.AuthMiddleware>();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        Setup(app);

        return app;
    }

    private static string GetConnectionString()
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

    private static IServiceCollection AddJwt(this IServiceCollection services)
    {
        string key = Environment.GetEnvironmentVariable("JWT_PRIVATE_KEY")
            ?? throw new Exception("Missing JWT private key");
        JwtHolder jwtHolder = new JwtHolder(key);

        services
            .AddSingleton<JwtHolder>(sp => jwtHolder)
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt => opt.TokenValidationParameters = Utils.JwtHandler.GetTokenParams(jwtHolder));

        return services;
    }
    private static IServiceCollection AddSecret(this IServiceCollection services)
    {
        string secret = Environment.GetEnvironmentVariable("SECRET")
            ?? throw new Exception("Missing secret");

        SecretHolder secretHolder = new SecretHolder(secret);
        services.AddSingleton<SecretHolder>(sp => secretHolder);

        return services;
    }

    private static void Setup(WebApplication app)
    {
        // scoped
        using (IServiceScope serviceScope = app.Services.CreateScope())
        {
            serviceScope.ServiceProvider.GetService<Database.DB>()!.Database.Migrate();
        }
    }
}

