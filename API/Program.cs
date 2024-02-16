using API.Extensions;
using Application;
using Infrastructure;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiOptions()
    .AddApiServices()
    .AddApiCors()
    .AddApplication()
    .AddPersistence()
    .AddInfrastructure();

var app = builder.Build();

await app.InitDbAsync();

app.ApplyApiCors();
app.ApplyApiMiddlewares();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
