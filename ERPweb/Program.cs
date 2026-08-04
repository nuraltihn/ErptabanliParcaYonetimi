using ERPweb.Components;
using Erpyonetimi.Application.Services;
using Erpyonetimi.Context;
using Erpyonetimi.Data;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Data.Repositories;
using Erpyonetimi.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Blazor Server Bileşenleri
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Veritabanı Bağlantısı
builder.Services.AddDbContext<ErpDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Servis Kayıtları
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<KategoriService>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TedarikciService>();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();