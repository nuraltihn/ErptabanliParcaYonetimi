
using ERPWeb.Data;
using ERPweb.Components;
using ERPweb.Data; // 1. Burayı ekledik (DbContext'i tanısın diye)
using Microsoft.EntityFrameworkCore; // 2. Entity Framework kütüphanesini ekledik

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
  
  .AddInteractiveServerComponents();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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