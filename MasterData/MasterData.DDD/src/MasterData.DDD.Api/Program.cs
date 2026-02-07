using MasterData.DDD.Application.Services;
using MasterData.DDD.Domain.Aggregates.ProvinceAggregate;
using MasterData.DDD.Domain.SeedWork;
using MasterData.DDD.Infrastructure.Persistence;
using MasterData.DDD.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MasterData DDD API", Version = "v1" });
});

// Database Configuration
builder.Services.AddDbContext<DddDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DddConnection"),
        b => b.MigrationsAssembly("MasterData.DDD.Infrastructure")));

// DDD Pattern Registration
builder.Services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<DddDbContext>());
builder.Services.AddScoped<IProvinceRepository, ProvinceRepository>();
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
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MasterData DDD API v1"));
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
