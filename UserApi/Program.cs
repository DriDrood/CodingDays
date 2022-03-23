using CodingDays.UserApi;
using Microsoft.AspNetCore.Builder;

WebApplication.CreateBuilder(args)
    .ConfigureServices()
    .Build()
    .Configure()
    .Run();
