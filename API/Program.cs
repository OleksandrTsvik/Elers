using API.Extensions;
using Application;
using Infrastructure;
using Persistence;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiOptions()
    .AddApiServices()
    .AddApiCors()
    .AddApplication()
    .AddPersistence()
    .AddInfrastructure();

WebApplication app = builder.Build();

await app.InitDbAsync();

app.ApplyApiCors();
app.ApplyApiCookie();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLocalization();

app.ApplyApiMiddlewares();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
