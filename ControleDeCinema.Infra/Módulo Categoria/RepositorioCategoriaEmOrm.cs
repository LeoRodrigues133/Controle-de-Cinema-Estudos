using ControleDeCinema.Domínio;
using ControleDeCinema.Infra.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace ControleDeCinema.Infra.Módulo_Categoria;
public class RepositorioCategoriaEmOrm : RepositorioBaseEmOrm<Categoria>, IRepositorioCategoria
{
    public RepositorioCategoriaEmOrm(CinemaDbContext dbContext) : base(dbContext)
    {
    }

    protected override DbSet<Categoria> ObterRegistro()
    {
        return _dbContext.Categorias;
    }

    public Categoria SelecionarPorId(int id)
    {
        return ObterRegistro()
            .FirstOrDefault(x => x.Id == id)!;
    }
    public List<Categoria> SelecionarTodos()
    {
        return ObterRegistro()
            .ToList();
    }

    public List<Categoria> Filtrar(Func<Categoria, bool> predicate)
    {
        return ObterRegistro()
            .Where(predicate)
            .ToList();
    }

}
