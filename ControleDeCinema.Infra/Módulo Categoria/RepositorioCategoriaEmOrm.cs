using ControleDeCinema.Domínio;
using ControleDeCinema.Infra.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace ControleDeCinema.Infra.Módulo_Categoria;
public class RepositorioCategoriaEmOrm : RepositorioBaseEmOrm<Categoria>, IRepositorioCategoria
{
    readonly CinemaDbContext _dbContext;

    public RepositorioCategoriaEmOrm(CinemaDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    protected override DbSet<Categoria> ObterRegistro()
    {
        return _dbContext.Categorias;
    }

    public Categoria SelecionarPorId(int id)
    {
        return ObterRegistro()
            .Include(x => x.Filmes)
            .FirstOrDefault(x => x.Id == id)!;
    }
    public List<Categoria> SelecionarTodos()
    {
        return ObterRegistro()
            .Include(x => x.Filmes)
            .ToList();
    }

    public List<Categoria> Filtrar(Func<Categoria, bool> predicate)
    {
        throw new NotImplementedException();
    }

}
