using ASPNETDockerRestAPI.Models;
using ASPNETDockerRestAPI.Business;
using ASPNETDockerRestAPI.Business.Implementations;
using Microsoft.EntityFrameworkCore;
using ASPNETDockerRestAPI.Repository;
using ASPNETDockerRestAPI.Repository.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning();

builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(
    builder.Configuration["MySQLConnection:MySQLConnectionString"],
    ServerVersion.AutoDetect(builder.Configuration["MySQLConnection:MySQLConnectionString"])
));

builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
