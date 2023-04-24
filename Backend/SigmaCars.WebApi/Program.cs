using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SigmaCars.Application.Behaviors;
using SigmaCars.Application.Features.Authentication;
using SigmaCars.Application.Features.CarModel.Commands;
using SigmaCars.Application.Persistence;
using SigmaCars.Domain.Models;
using SigmaCars.Infrastructure.Authentication;
using SigmaCars.WebApi.Middlewares;
using static SigmaCars.WebApi.ProgramHelpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(ConfigureSwaggerGen);

// Database
builder.Services.AddDbContext<SigmaCarsDbContext>(options => ConfigureDbContext(options, builder));

// Middlewares
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

// Fluent validation
builder.Services.AddValidatorsFromAssemblyContaining<CreateCarModelValidator>(ServiceLifetime.Transient);

// Mediator
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<CreateCarModelHandler>());
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>() 
                  // ReSharper disable once StringLiteralTypo
                  ?? throw new InvalidOperationException("Jwt settings not found, check appsettings.json");
builder.Services.AddSingleton(jwtSettings);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
            
            ValidateLifetime = true
        });
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();

// Authorization
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger(options => options.RouteTemplate = "schema/{documentName}");
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();