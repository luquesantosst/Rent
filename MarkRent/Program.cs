using MarkRent.Application.Services;
using MarkRent.Domain.Entities.Enums;
using MarkRent.Domain.Interfaces.Messaging;
using MarkRent.Domain.Interfaces.Repository;
using MarkRent.Domain.Interfaces.Service;
using MarkRent.Domain;
using MarkRent.Infra.Context;
using MarkRent.Infra.Messaging;
using MarkRent.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using MarkRent.Domain.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();

builder.Services.AddScoped<IDeliveryAgentService, DeliveryAgentService>();
builder.Services.AddScoped<IDeliveryAgentRepository, DeliveryAgentRepository>();
builder.Services.AddScoped<IStorageService, LocalStorageService>();

builder.Services.AddScoped<IHireService, HireService>();
builder.Services.AddScoped<IHireRepository, HireRepository>();

builder.Services.AddScoped<IPriceDayService, PriceDayService>();
builder.Services.AddScoped<IPriceDayRepository, PriceDayRepository>();


// Registrar VehicleCreatedConsumer como Scoped
builder.Services.AddScoped<VehicleCreatedConsumer>();

// Registrar o VehicleCreatedConsumer como serviço em segundo plano (Background Service)
builder.Services.AddHostedService<VehicleCreatedConsumer>();

// Carregar configurações do rabbit
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));

// Registrar a implementação do publisher
builder.Services.AddScoped<IVehiclePublisher, VehicleCreatedPublisher>();

// Configuração do meu context para o PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração do JSON para suportar enum como string
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Total",
        builder =>
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MarkRent API", Version = "v1" });

    // Mapeamento do enum TypeCNH
    c.MapType<TypeCNH>(() => new OpenApiSchema
    {
        Type = "string",
        Enum = Enum.GetValues(typeof(TypeCNH))
                   .Cast<TypeCNH>()
                   .Select(e => new OpenApiString(e.ToString()))
                   .Cast<IOpenApiAny>()
                   .ToList()
    });
});

builder.Services.AddControllers();
// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ativar CORS
app.UseCors("Total");

// Configuração do Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Configuração do pipeline HTTP
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//middleware para customizar as validações do projeto
app.UseExceptionHandler(appBuilder =>
{
    appBuilder.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        int statusCode = StatusCodes.Status500InternalServerError;

        if (exception is ArgumentException)
        {
            statusCode = StatusCodes.Status400BadRequest;
        }
        else if (exception is KeyNotFoundException)
        {
            statusCode = StatusCodes.Status404NotFound;

        } else if (exception is ConflictException)
        {
            statusCode = StatusCodes.Status409Conflict;
        }

        context.Response.StatusCode = statusCode;

        var errorResponse = new
        {
            message = exception?.Message ?? "Dados inválidos"
        };

        await context.Response.WriteAsJsonAsync(errorResponse);
    });
});


app.Run();
