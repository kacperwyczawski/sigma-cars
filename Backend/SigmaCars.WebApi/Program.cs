using SigmaCars.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwaggerGen();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddExceptionHandling();
builder.Services.AddValidation();
builder.Services.AddMediator();
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCustomSwagger();
app.UseExceptionHandling();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();