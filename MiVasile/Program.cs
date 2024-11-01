using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.VisualBasic;
using MiVasile.Components;
using MiVasile.Domain.Interfaces;
using MiVasile.Domain.Models;



var builder = WebApplication.CreateBuilder(args);

//Servicios Autentecación y Autorizacion
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthentication(Constantes.AuthScheme)
    .AddCookie(Constantes.AuthScheme, options =>
    {
        options.Cookie.Name = Constantes.AuthCoockie;
        options.LoginPath = "/auth/login";
        options.AccessDeniedPath = "/auth/alert-account";
        options.LogoutPath = "/auth/logout";

        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;

        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.SlidingExpiration = true;
    });
builder.Services.AddAuthorization();


//Servicios de Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


//Servicios de Aplicacion
//HttpClient
builder.Services.AddScoped(sp =>
{
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HttpClient { BaseAddress = new Uri(navigationManager.BaseUri) };
});
//HttpContext para servicio de sesiones
builder.Services.AddHttpContextAccessor();

//Agregar Controladores 
builder.Services.AddControllers();

//Configurar CIRCUIT para ver los errores en el navegador
builder.Services.Configure<CircuitOptions>(options =>
{
    options.DetailedErrors = true;
});




var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // Configura HSTS para asegurar las conexiones HTTPS en producción.
}

// 1. Redirigir HTTP a HTTPS
app.UseHttpsRedirection();

// 2. Servir archivos estáticos
app.UseStaticFiles();

// 3. Habilitar el enrutamiento
app.UseRouting();

// 4. Autenticación y Autorización
app.UseAuthentication()
    .UseAuthorization();

// 5. Protección contra ataques CSRF
app.UseAntiforgery();

// 6. Mapear controladores
app.MapControllers();

// 7. Mapear componentes Razor con modo interactivo
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
