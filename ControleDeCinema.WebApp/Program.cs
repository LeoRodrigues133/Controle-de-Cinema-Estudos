using ControleDeCinema.Aplicação;
using ControleDeCinema.Domínio;
using ControleDeCinema.Infra.Compartilhado;
using ControleDeCinema.Infra.Módulo_Categoria;
using ControleDeCinema.Infra.Módulo_Filme;
using ControleDeCinema.Infra.Módulo_Sala;
using ControleDeCinema.WebApp.Mapping.Resolvers;
using System.Reflection;

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

        builder.Services.AddScoped<SalaResolver>();
        builder.Services.AddScoped<CategoriaResolver>();
        builder.Services.AddScoped<FilmeResolver>();

        builder.Services.AddScoped<SalaService>();
        builder.Services.AddScoped<CategoriaService>();
        builder.Services.AddScoped<FilmeService>();

        builder.Services.AddAutoMapper(config =>
        {
            config.AddMaps(Assembly.GetExecutingAssembly());
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
