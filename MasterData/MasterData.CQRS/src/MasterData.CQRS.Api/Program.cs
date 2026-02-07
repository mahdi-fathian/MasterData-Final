using MasterData.CQRS.Application.Interfaces;
using MasterData.CQRS.Infrastructure.Persistence;
using MasterData.CQRS.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MasterData CQRS API", Version = "v1" });
});

// Database Configuration
builder.Services.AddDbContext<CqrsDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("CqrsConnection"),
        b => b.MigrationsAssembly("MasterData.CQRS.Infrastructure")));

// MediatR Configuration
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(MasterData.CQRS.Application.Commands.RegisterProvinceCommand).Assembly);
});

// Repository Registration
builder.Services.AddScoped<IProvinceRepository, ProvinceRepository>();
builder.Services.AddScoped<IProvinceReadRepository, ProvinceReadRepository>();

// Logging Configuration
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MasterData CQRS API v1"));
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
