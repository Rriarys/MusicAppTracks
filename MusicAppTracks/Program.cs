using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using MusicAppTracks.Data;
using MusicAppTracks.Interfaces;
using MusicAppTracks.Services.BlobStorage;
using MusicAppTracks.Services.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Настройка подключения к базе данных PostgreSQL через Entity Framework Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // { PostgreSQL }

// Регистрация сервисов приложения (внедрение зависимостей)
builder.Services.AddScoped<ITrackDbService, TrackDbService>();

// Регистрация клиента Azure Blob Storage
builder.Services.AddSingleton(x =>
    new BlobServiceClient(builder.Configuration.GetValue<string>("AzureStorage:ConnectionString")));
// { Azurite }
// Регистрация сервиса BlobStorage
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseExceptionHandler("/Track/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// Настройка маршрутизации MVC (по умолчанию контроллер Track, действие Index)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Track}/{action=Index}/{id?}");


app.Run();
