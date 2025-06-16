using System.Reflection;
using ControleDeCinema.Domínio;
using Microsoft.AspNetCore.Identity;
using ControleDeCinema.Infra.Módulo_Sala;
using ControleDeCinema.Infra.Módulo_Filme;
using ControleDeCinema.Infra.Módulo_Sessão;
using ControleDeCinema.Infra.Compartilhado;
using ControleDeCinema.Domínio.Módulo_Sessão;
using ControleDeCinema.Infra.Módulo_Categoria;
using ControleDeCinema.WebApp.Mapping.Resolvers;
using Microsoft.AspNetCore.Authentication.Cookies;
using ControleDeCinema.Aplicação.Services.CategoriaService;
using ControleDeCinema.Aplicação.Services.FilmeService;
using ControleDeCinema.Aplicação.Services.SalaService;
using ControleDeCinema.Aplicação.Services.SessãoService;
using ControleDeCinema.Aplicação.Services.AutenticaçãoService;

namespace ControleDeCinema.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Injeçao de Dependências
        builder.Services.AddDbContext<CinemaDbContext>();

        builder.Services.AddScoped<IRepositorioSala, RepositorioDeSalaOrm>();
        builder.Services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmOrm>();
        builder.Services.AddScoped<IRepositorioFilme, RepositorioFilmeEmOrm>();
        builder.Services.AddScoped<IRepositorioSessao, RepositorioSessaoEmOrm>();

        builder.Services.AddScoped<SalaResolver>();
        builder.Services.AddScoped<CategoriaResolver>();
        builder.Services.AddScoped<FilmeResolver>();
        builder.Services.AddScoped<UsuarioResolver>();

        builder.Services.AddScoped<SalaService>();
        builder.Services.AddScoped<CategoriaService>();
        builder.Services.AddScoped<FilmeService>();
        builder.Services.AddScoped<SessaoService>();

        builder.Services.AddAutoMapper(config =>
        {
            config.AddMaps(Assembly.GetExecutingAssembly());
        });


        builder.Services.AddIdentity<Usuario, Perfil>()
            .AddEntityFrameworkStores<CinemaDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.Configure<IdentityOptions>(opt =>
        {
            //Se não houver configuração: Default = Password1@

            opt.Password.RequireDigit = false;
            opt.Password.RequiredLength = 1;
            opt.Password.RequireLowercase = false;
            opt.Password.RequiredUniqueChars = 1;
            opt.Password.RequireNonAlphanumeric = false;
        });

        builder.Services.AddScoped<AutenticacaoService>();

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(opt =>
            {
                opt.Cookie.Name = "AspNetCore.Cookies"; // Default cookie AspNet;
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Tempo de duração da sessão do usuário;
                opt.SlidingExpiration = true; // Renova a sessão caso o usuário faça um novo request para o servidor;
            });

        builder.Services.ConfigureApplicationCookie(opt =>
        {
            opt.LoginPath = "/Autenticacao/Login";
            opt.AccessDeniedPath = "/Autenticacao/AcessoNegado";
        });
        #endregion

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
