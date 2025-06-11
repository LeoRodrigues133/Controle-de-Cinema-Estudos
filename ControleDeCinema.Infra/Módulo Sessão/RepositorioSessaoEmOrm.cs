using ControleDeCinema.Domínio;
using ControleDeCinema.Domínio.Módulo_Sessão;
using ControleDeCinema.Infra.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace ControleDeCinema.Infra.Módulo_Sessão;
public class RepositorioSessaoEmOrm : RepositorioBaseEmOrm<Sessão>, IRepositorioSessao
{
    public RepositorioSessaoEmOrm(CinemaDbContext dbContext) : base(dbContext)
    {
    }

    public Sessão SelecionarPorId(int id)
    {
        return ObterRegistro()
            .Include(x => x.Filme)
            .ThenInclude(y => y.Categoria)
            .Include(x => x.Sala)
            .Include(x => x.Assentos)
            .FirstOrDefault(x => x.Id == id)!;
    }

    public List<Sessão> SelecionarTodos()
    {
        return ObterRegistro()
            .Include(x => x.Filme)
            .ThenInclude(y => y.Categoria)
            .Include(x => x.Sala)
            .Include(x => x.Assentos)
            .ToList();
    }

    public List<Sessão> Filtrar(Func<Sessão, bool> predicate)
    {
        throw new NotImplementedException();
    }

    protected override DbSet<Sessão> ObterRegistro()
    {
        return _dbContext.Sessaos;
    }
}
