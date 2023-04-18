using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SigmaCars.WebApi;

public static class ProgramHelpers
{
    public static void ConfigureDbContext(DbContextOptionsBuilder options, WebApplicationBuilder builder) =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")
                          ?? throw new InvalidOperationException("Connection string not found"));

    public static void ConfigureSwaggerGen(SwaggerGenOptions options) =>
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Sigma cars web API",
            Description = "Backend API for Sigma cars web application",
            License = new OpenApiLicense
            {
                Name = "GNU AGPLv3",
                Url = new Uri("https://github.com/kacperwyczawski/sigma-cars/blob/main/LICENSE")
            }
        });
}