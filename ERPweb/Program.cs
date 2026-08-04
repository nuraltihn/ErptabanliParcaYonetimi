using ERPweb.Components;
using Erpyonetimi.Application.Services;
using Erpyonetimi.Application.Services.Interfaces;
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
builder.Services.AddScoped<IUsersService,UsersService>();
builder.Services.AddScoped<IKategoriService,KategoriService>();
builder.Services.AddScoped<IDashboardService,DashboardService>();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<ITedarikciService,TedarikciService>();
//nisa değişiklik

// Repository kayıtları - uygulama servislerinin ihtiyaç duyduğu repository implementasyonları
// eksik olduğu için DI doğrulaması başarısız oluyordu. Scoped, DbContext ile uyumlu.
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IKategoriRepository, KategoriRepository>();
builder.Services.AddScoped<ITedarikciRepository, TedarikciRepository>();
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