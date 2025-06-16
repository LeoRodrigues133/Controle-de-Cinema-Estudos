using ControleDeCinema.Domínio;
using Microsoft.EntityFrameworkCore;
using ControleDeCinema.Infra.Compartilhado;

namespace ControleDeCinema.Infra.Módulo_Filme;
public class RepositorioFilmeEmOrm : RepositorioBaseEmOrm<Filme>, IRepositorioFilme
{

    public RepositorioFilmeEmOrm(CinemaDbContext dbContext) : base(dbContext)
    {
    }

    public Filme SelecionarPorId(int id)
    {
        return ObterRegistro()
            .Include(x => x.Categoria)
            .FirstOrDefault(x => x.Id == id)!;
    }

    public List<Filme> SelecionarTodos()
    {
        return ObterRegistro()
            .Include(x => x.Categoria)
            .ToList();
    }

    public List<Filme> Filtrar(Func<Filme, bool> predicate)
    {
        return ObterRegistro()
            .Include(x => x.Categoria)
            .Where(predicate)
            .ToList();
    }

    protected override DbSet<Filme> ObterRegistro()
    {
        return _dbContext.Filmes;
    }
}
