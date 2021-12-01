using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();
app.UseRouting();
app.UseEndpoints(routes => routes.MapControllerRoute("default", "api/register", new { action = "Get", controller = "Register" }));

app.Run();
