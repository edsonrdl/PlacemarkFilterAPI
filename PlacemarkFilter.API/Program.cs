
using Microsoft.OpenApi.Models;
using PlacemarkFilter.Application.Services;
using PlacemarkFilter.Application.FilterStrategy;
using PlacemarkFilter.Domain.Interfaces.Services;
using PlacemarkFilter.Domain.Interfaces.UseCases;
using PlacemarkFilter.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlacemarkFilter API", Version = "v1" });
});


builder.Services.AddScoped<IKmlRepository, KmlRepository>();


builder.Services.AddSingleton<Dictionary<string, IFilterStrategy>>(provider =>
{
    return new Dictionary<string, IFilterStrategy>
    {
        { "CLIENTE", new ClientFilterStrategy() },
        { "SITUACAO", new SituationFilterStrategy() },
        { "BAIRRO", new BairroFilterStrategy() },
        { "REFERENCIA", new ReferenciaFilterStrategy() },
        { "RUA/CRUZAMENTO", new RuaCruzamentoFilterStrategy() }
    };
});


builder.Services.AddScoped<IKmlService>(provider =>
{
    var kmlRepository = provider.GetRequiredService<IKmlRepository>();
    var filterStrategies = provider.GetRequiredService<Dictionary<string, IFilterStrategy>>();
    return new KmlService(kmlRepository, filterStrategies);
});

var app = builder.Build();


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
