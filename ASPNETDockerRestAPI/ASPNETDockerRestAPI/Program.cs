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
using ASPNETDockerRestAPI.Hypermedia.Filters;
using ASPNETDockerRestAPI.Hypermedia.Enricher;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;
using ASPNETDockerRestAPI.Services;
using ASPNETDockerRestAPI.Services.Implementations;
using ASPNETDockerRestAPI.Repository.User;
using ASPNETDockerRestAPI.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

var appName = "RESTFUL API with ASP.NET Core 8";
var appVersion = "v1";
var appDescription = $"RESTFUL API with ASP.NET Core 8 - {appVersion}";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

// Configure Token Configuration
var tokenConfiguration = new TokenConfiguration();
var rawTokenConfigurations = new ConfigureFromConfigurationOptions<TokenConfiguration>(builder.Configuration.GetSection("TokenConfigurations"));

rawTokenConfigurations.Configure(tokenConfiguration);
builder.Services.AddSingleton(tokenConfiguration);

// Configure Authentication and Authorization
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenConfiguration.Issuer,
        ValidAudience = tokenConfiguration.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.Secret))
    };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build()
);

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

// Configure controllers + versioning
builder.Services.AddControllers();
builder.Services.AddApiVersioning();

// Configure database
var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];

builder.Services.AddDbContext<MySqlContext>(options => options.UseMySql(
    connection,
    ServerVersion.AutoDetect(connection)
));

if (builder.Environment.IsDevelopment())
{
    MigrateDatabase(connection);
}

// Configure content negotiation
builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", "application/json");
}).AddXmlSerializerFormatters();

var filterOptions = new HypermediaFilterOptions();

filterOptions.ContentResponseEnrichers.Add(new PersonEnricher());
filterOptions.ContentResponseEnrichers.Add(new BookEnricher());

builder.Services.AddSingleton(filterOptions);

// Configure DI
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();
builder.Services.AddScoped<ILoginBusiness, LoginBusinessImplementation>();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IPersonParser, PersonParserImplementation>();
builder.Services.AddScoped<IBookParser, BookParserImplementation>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositoryImplementation<>));

// Configure swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(appVersion, new OpenApiInfo
    {
        Title = appName,
        Version = appVersion,
        Description = appDescription,
        Contact = new OpenApiContact
        {
            Name = "Vinicius Rech",
            Url = new Uri("https://github.com/vinitrech")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

// app.UseCors() MUST be placed after UseHttpsRedirection
app.UseCors();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{appName} - {appVersion}");
});

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");

app.UseRewriter(option);

app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");

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
