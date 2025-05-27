using ControleDeCinema.Domínio;
using ControleDeCinema.Infra.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace ControleDeCinema.Infra.Módulo_Sala;
public class RepositorioDeSalaOrm : RepositorioBaseEmOrm<Sala>, IRepositorioSala
{
    public RepositorioDeSalaOrm(CinemaDbContext dbContext) : base(dbContext)
    {
    }
    protected override DbSet<Sala> ObterRegistro()
    {
        return _dbContext.Salas;
    }

    public Sala SelecionarPorId(int id)
    {
        return ObterRegistro()
            .FirstOrDefault(x => x.Id == id)!;
    }

    public List<Sala> SelecionarTodos()
    {
        return ObterRegistro()
            .ToList();
    }
    public List<Sala> Filtrar(Func<Sala, bool> predicate)
    {
        throw new NotImplementedException();
    }

}
