using Capstone.Api.Services.Helpers;
using Capstone.Data.Entities.Models;
using Capstone.Repositories.Classes;
using Capstone.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CapstoneDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Production"));
});

var allowedOrigins = "_allowedOrigins";
builder.Services.AddCors(options => 
    options.AddPolicy(name: allowedOrigins,
                      builder =>
                      {
                          builder.AllowAnyOrigin();
                          //builder.WithOrigins("http://localhost:3000", "https://gco-devopsteam-core.azurewebsites.net");
                          //builder.AllowCredentials();
                          builder.AllowAnyHeader();
                          builder.AllowAnyMethod();
                      })
);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<ICreditScoreRepository, CreditScoreRepository>();
builder.Services.AddScoped<ICriminalRecordRepository, CriminalRecordRepository>();
builder.Services.AddScoped<IEducationRecordRepository, EducationRecordRepository>();
builder.Services.AddScoped<IEmploymentHistoryRepository, EmploymentHistoryRepository>();
builder.Services.AddScoped<IIdentityDetailRepository, IdentityDetailRepository>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Settings:SecretKey").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAutoMapper(typeof(Program));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Authorize",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors(allowedOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
