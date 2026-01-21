using APIWEB.src.Shared.Infrastructure.Configurations;
using APIWEB.src.Features.User.Domain.Ports;
using APIWEB.src.Features.User.Adapters.Out.Persistence;
using APIWEB.src.Features.User.Adapters.In.Rest;
using APIWEB.src.Features.User.Application.ports;
using APIWEB.src.Features.Auth.Domain.Ports;
using APIWEB.src.Features.Auth.Infrastructure.Ports;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Configurar JWT
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

// Adicionar FluentValidation
builder.Services.AddScoped<IValidator<CreateUserRequest>, CreateUserRequestValidator>();

// Configurar o DbContext com PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Registrar o repositório
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Registrar os use cases
builder.Services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
builder.Services.AddScoped<IFindUserByIdUseCase, FindUserByIdUseCase>();
builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();
builder.Services.AddScoped<IFindUserByEmailUseCase, FindUserByEmailUseCase>();

// Registrar os serviços de autenticação
builder.Services.AddScoped<IHashService, HashService>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
