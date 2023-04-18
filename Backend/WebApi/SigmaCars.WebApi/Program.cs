using FluentValidation;
using MediatR;
using SigmaCars.Application.Behaviors;
using SigmaCars.Application.Features.CarModel.Commands;
using SigmaCars.Application.Persistence;
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

var app = builder.Build();

app.UseSwagger(options => options.RouteTemplate = "schema/{documentName}");
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();