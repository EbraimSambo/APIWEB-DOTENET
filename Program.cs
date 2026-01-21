using APIWEB.src.Shared.Infrastructure.Configurations;
using APIWEB.src.Features.User.Domain.Ports;
using APIWEB.src.Features.User.Adapters.Out.Persistence;
using APIWEB.src.Features.User.Adapters.In.Rest;
using APIWEB.src.Features.User.Application.ports;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Adicionar FluentValidation
builder.Services.AddScoped<IValidator<CreateUserRequest>, CreateUserRequestValidator>();

// Configurar o DbContext com PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Registrar o repositório
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Registrar o use case
builder.Services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
