using APIWEB.src.Shared.Infrastructure.DI;
using APIWEB.src.Shared.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurar dependências
SwaggerConfiguration.ConfigureSwagger(builder.Services);
AuthenticationConfiguration.ConfigureAuthentication(builder.Services, builder.Configuration);
ValidationConfiguration.ConfigureValidation(builder.Services);
DIContainer.ConfigureDependencies(builder.Services, builder.Configuration);

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
