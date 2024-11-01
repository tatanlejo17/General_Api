using FluentValidation;
using General_api.DTOs;
using General_api.Interface;
using General_api.Models;
using General_api.Services;
using General_api.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddKeyedSingleton<IRandomService, RandomService>("serviceSingleton");
builder.Services.AddKeyedScoped<IRandomService, RandomService>("serviceScoped");
builder.Services.AddKeyedTransient<IRandomService, RandomService>("serviceTransient");
builder.Services.AddKeyedScoped<ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>, BeerService>("BeerService");

// Add service to JsonPlaceholder
builder.Services.AddScoped<IPlaceholderService, PlaceholderService>();
builder.Services.AddHttpClient<IPlaceholderService, PlaceholderService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
});

// Inyección del Contexto de la BD
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

// Inyección de FluentValidator
builder.Services.AddScoped<IValidator<BeerInsertDto>, BeerInsertValidator>();
builder.Services.AddScoped<IValidator<BeerUpdateDto>, BeerUpdateValidator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
