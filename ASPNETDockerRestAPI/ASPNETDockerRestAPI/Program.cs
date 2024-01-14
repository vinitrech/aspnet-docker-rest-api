using ASPNETDockerRestAPI.Models;
using ASPNETDockerRestAPI.Business;
using ASPNETDockerRestAPI.Business.Implementations;
using Microsoft.EntityFrameworkCore;
using ASPNETDockerRestAPI.Repository.Implementations;
using MySqlConnector;
using EvolveDb;
using Serilog;
using ASPNETDockerRestAPI.Repository.Generic;
using ASPNETDockerRestAPI.Parsers;
using ASPNETDockerRestAPI.Parsers.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning();

var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];

builder.Services.AddDbContext<MySqlContext>(options => options.UseMySql(
    connection,
    ServerVersion.AutoDetect(connection)
));

if (builder.Environment.IsDevelopment())
{
    MigrateDatabase(connection);
}

builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();
builder.Services.AddScoped<IPersonParser, PersonParserImplementation>();
builder.Services.AddScoped<IBookParser, BookParserImplementation>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositoryImplementation<>));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void MigrateDatabase(string? connection)
{
    try
    {
        var evolveConnection = new MySqlConnection(connection);
        var evolve = new Evolve(evolveConnection, Log.Information)
        {
            Locations = ["Database/Migrations", "Database/Datasets"],
            IsEraseDisabled = true
        };

        evolve.Migrate();
    }
    catch (Exception e)
    {
        Log.Error("Database migration error", e);
        throw;
    }
}
