using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using MusicAppTracks.Data;
using MusicAppTracks.Interfaces;
using MusicAppTracks.Services.BlobStorage;
using MusicAppTracks.Services.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// ��������� ����������� � ���� ������ PostgreSQL ����� Entity Framework Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // { PostgreSQL }

// ����������� �������� ���������� (��������� ������������)
builder.Services.AddScoped<ITrackDbService, TrackDbService>();

// ����������� ������� Azure Blob Storage
builder.Services.AddSingleton(x =>
    new BlobServiceClient(builder.Configuration.GetValue<string>("AzureStorage:ConnectionString")));
// { Azurite }
// ����������� ������� BlobStorage
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

// ��������� ������������� MVC (�� ��������� ���������� Track, �������� Index)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Track}/{action=Index}/{id?}");


app.Run();
