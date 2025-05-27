using ControleDeCinema.Domínio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ControleDeCinema.Infra.Compartilhado;
public class CinemaDbContext : DbContext
{
    public DbSet<Sala> Salas { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Filme> Filmes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = config
            .GetConnectionString("SqlServer");

        optionsBuilder.UseSqlServer(connectionString);

        #region Configurações adicionais para o EF Core

        //Gera log detalhado sobre a migração

        //optionsBuilder.LogTo(Console.WriteLine)
        //    .EnableSensitiveDataLogging()
        //    .EnableDetailedErrors();

        //Carrega os dados com base no que o usuário acessa

        //optionsBuilder.UseLazyLoadingProxies();

        //Faz com que o EF Core não rastreie as entidades

        //optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        #endregion

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfiguration(new MapeadorSala());
        modelBuilder.ApplyConfiguration(new MapeadorCategoria());
        modelBuilder.ApplyConfiguration(new MapeadorFilme());

        base.OnModelCreating(modelBuilder);
    }
}
