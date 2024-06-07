using System.Reflection;
using System.Text;
using _1_API.Mapper;
using _1_API.Middleware;
using _2_Domain;
using _3_Data;
using _3_Data.Contexts;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Propertunity API Documentation v1.0.0",
            Description = "An ASP.NET Core Web API for managing Propertunity domain, data, and presentation layers.",
            Contact = new OpenApiContact
            {
                Name = "Contact the team",
                Url = new Uri("https://propertunity.com")
            },
            License = new OpenApiLicense
            {
                Name = "MIT License",
                Url = new Uri("https://opensource.org/licenses/MIT")
            },
            TermsOfService = new Uri("https://propertunity.com/terms-of-service")
        });
        
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    }
);

builder.Services.AddScoped<IUserRegisterData, UserRegisterData>();
builder.Services.AddScoped<IUserRegisterDomain, UserRegisterDomain>();
builder.Services.AddScoped<IUserAthenticationData, UserAthenticationData>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ISearchDomain, SearchDomain>();
builder.Services.AddScoped<ISearchData, SearchData>();
builder.Services.AddScoped<IPublicationDomain, PublicationDomain>();
builder.Services.AddScoped<IPublicationData, PublicationData>();
builder.Services.AddScoped<IUserManagerDomain, UserManagerDomain>();
builder.Services.AddScoped<IUserManagerData, UserManagerData>();

var key = builder.Configuration.GetValue<string>("JwtSettings:key");
var keyBytes = Encoding.ASCII.GetBytes(key);
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAutoMapper(
    typeof(RequestToModels)
);

//  Connection to database.
var connectionString = builder.Configuration.GetConnectionString("propertunityDataCenterConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
builder.Services.AddDbContext<PropertunityDataCenterContext>(
    dbContextOptions =>
    {
        dbContextOptions.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString),
            options => options.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: System.TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null)
        );
    });

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();


using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<PropertunityDataCenterContext>())
{
    context.Database.EnsureCreated();
}

//  Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();