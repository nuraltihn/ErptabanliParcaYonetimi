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
builder.Services.AddScoped<IParcaService,ParcaService>();
builder.Services.AddScoped<IStokHareketService,StokHareketService>();
builder.Services.AddScoped<IMusteriService,MusteriService>();
builder.Services.AddScoped<IDepoService, DepoService>();
builder.Services.AddScoped<IRafService,RafService>();
builder.Services.AddScoped<IStokHareketService,StokHareketService>();
builder.Services.AddScoped<ISiparisService,SiparisService>();
builder.Services.AddScoped<ISiparisDetayService,SiparisDetayService>();
builder.Services.AddScoped<ILogService, LogService>();


builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IKategoriRepository, KategoriRepository>();
builder.Services.AddScoped<ITedarikciRepository, TedarikciRepository>();
builder.Services.AddScoped<IParcaRepository, ParcaRepository>();
builder.Services.AddScoped<IMusteriRepository,MusteriRepository>();
builder.Services.AddScoped<IDepoRepository, DepoRepository>();  
builder.Services.AddScoped<IRafRepository, RafRepository>();
builder.Services.AddScoped<IStokHareketRepository, StokHareketRepository>();
builder.Services.AddScoped<ISiparisRepository, SiparisRepository>();
builder.Services.AddScoped<ISiparisDetayRepository, SiparisDetayRepository>();  
builder.Services.AddScoped<ILogRepository, LogRepository>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ErpDbContext>();
    Datalar.Seed(db);
}
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