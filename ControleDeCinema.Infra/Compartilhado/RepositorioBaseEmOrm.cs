using ControleDeCinema.Domínio;
using ControleDeCinema.Infra.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace ControleDeCinema.Infra;
public abstract class RepositorioBaseEmOrm<Generics> where Generics : EntidadeBase
{
    protected readonly CinemaDbContext _dbContext;

    public RepositorioBaseEmOrm(CinemaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected abstract DbSet<Generics> ObterRegistro();

    public void Inserir(Generics registro)
    {
        ObterRegistro().Add(registro);

        _dbContext.SaveChanges();
    }

    public void Editar(Generics registro)
    {
        ObterRegistro().Update(registro);

        _dbContext.SaveChanges();
    }

    public void Excluir(Generics registro)
    {
        ObterRegistro().Remove(registro);

        _dbContext.SaveChanges();
    }

    public virtual Generics? SelecionarPorId(int id)
    {
        return ObterRegistro().FirstOrDefault(x => x.Id == id);
    }

    public virtual List<Generics> SelecionarTodos()
    {
        return ObterRegistro().ToList();
    }
}
