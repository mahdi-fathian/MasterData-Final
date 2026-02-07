using MasterData.EventDriven.Application.Interfaces;
using MasterData.EventDriven.Application.Services;
using MasterData.EventDriven.Infrastructure.EventPublishing;
using MasterData.EventDriven.Infrastructure.Persistence;
using MasterData.EventDriven.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MasterData Event-Driven API", Version = "v1" });
});

// Database Configuration
builder.Services.AddDbContext<EventDrivenDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("EventDrivenConnection"),
        b => b.MigrationsAssembly("MasterData.EventDriven.Infrastructure")));

// Event-Driven Pattern Registration
builder.Services.AddScoped<IProvinceRepository, ProvinceRepository>();
builder.Services.AddScoped<IEventPublisher, OutboxEventPublisher>();
builder.Services.AddScoped<IProvinceService, ProvinceService>();

// Logging Configuration
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MasterData Event-Driven API v1"));
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
