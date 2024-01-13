using ASPNETDockerRestAPI.Models;
using ASPNETDockerRestAPI.Services;
using ASPNETDockerRestAPI.Services.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning();

builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(
    builder.Configuration["MySQLConnection:MySQLConnectionString"],
    ServerVersion.AutoDetect(builder.Configuration["MySQLConnection:MySQLConnectionString"])
));

builder.Services.AddScoped<IPersonService, PersonServiceImplementation>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
