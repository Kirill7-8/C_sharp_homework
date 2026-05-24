using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MusicCatalog.Application.Discs;
using MusicCatalog.Application.Discs.Abstractions;
using MusicCatalog.Application.Songs;
using MusicCatalog.Application.Songs.Abstractions;
using MusicCatalog.Infrastructure.Discs;
using MusicCatalog.Infrastructure.Songs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IDiscsRepository, DiscsRepository>();
builder.Services.AddSingleton<ISongsRepository, SongsRepository>();

builder.Services.AddScoped<DiscsService>();
builder.Services.AddScoped<SongsService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title       = "Music Catalog API",
        Version     = "v1",
        Description = "Каталог музыкальных компакт-дисков: диски, песни, поиск, сортировка"
    });
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(System.AppContext.BaseDirectory, xmlFile);
    if (System.IO.File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Music Catalog API V1");
    c.RoutePrefix = string.Empty;
});

app.UseRouting();
app.MapControllers();
app.Run();
