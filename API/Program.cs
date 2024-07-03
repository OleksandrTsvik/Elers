using API.Extensions;
using Application;
using Infrastructure;
using Persistence;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiOptions()
    .AddApiServices()
    .AddApiMiddlewares()
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

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToController("Index", "Fallback");

app.Run();
