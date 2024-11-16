using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PlacemarkFilter.Application.Services;
using PlacemarkFilter.Domain.Interfaces.Services;
using PlacemarkFilter.Domain.Interfaces.UseCases;
using PlacemarkFilter.infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Adiciona servi�os ao container
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configura��o do Swagger para documenta��o da API
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlacemarkFilter API", Version = "v1" });
});

// Inje��o de depend�ncias
builder.Services.AddScoped<IKmlService, KmlService>();
builder.Services.AddScoped<IKmlRepository, KmlRepository>();

var app = builder.Build();

// Configura o pipeline de requisi��o HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlacemarkFilter API v1");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.Run();
