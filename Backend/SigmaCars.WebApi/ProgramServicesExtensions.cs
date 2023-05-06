﻿using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SigmaCars.Application.Behaviors;
using SigmaCars.Application.Features.Authentication;
using SigmaCars.Application.Features.CarModel.Commands;
using SigmaCars.Application.Persistence;
using SigmaCars.Infrastructure.Authentication;
using SigmaCars.WebApi.Middlewares;

namespace SigmaCars.WebApi;

internal static class ProgramServicesExtensions
{
    internal static void AddCustomSwaggerGen(this IServiceCollection services) =>
        services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Sigma cars web API",
            Description = "Backend API for Sigma cars web application",
            License = new OpenApiLicense
            {
                Name = "GNU AGPLv3",
                Url = new Uri("https://github.com/kacperwyczawski/sigma-cars/blob/main/LICENSE")
            }
        }));

    internal static void AddDatabase(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<SigmaCarsDbContext>(options => options.UseNpgsql(
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string not found")));

    internal static void AddExceptionHandling(this IServiceCollection services) =>
        services.AddTransient<ExceptionHandlingMiddleware>();

    internal static void AddValidation(this IServiceCollection services) =>
        services.AddValidatorsFromAssemblyContaining<CreateCarModelValidator>(ServiceLifetime.Transient);

    internal static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssemblyContaining<CreateCarModelHandler>());

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    internal static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>()
                          // ReSharper disable once StringLiteralTypo
                          ?? throw new InvalidOperationException("Jwt settings not found, check appsettings.json");
        services.AddSingleton(jwtSettings);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

        services.AddScoped<IJwtGenerator, JwtGenerator>();
    }

    internal static void AddCustomCors(this IServiceCollection services) =>
        services.AddCors(options =>
            options.AddDefaultPolicy(policyBuilder =>
                policyBuilder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")));
}