using ControleDeCinema.Domínio;
using ControleDeCinema.Domínio.Módulo_Assento;
using ControleDeCinema.Infra.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace ControleDeCinema.Infra.Módulo_Assento;
public class RepositorioAssentoEmOrm : RepositorioBaseEmOrm<Assento>, IRepositorioAssento
{
    public RepositorioAssentoEmOrm(CinemaDbContext dbContext) : base(dbContext)
    {
    }
    public Assento SelecionarPorId(int id)
    {
        return ObterRegistro()
            .FirstOrDefault(x => x.Id == id)!;
    }

    protected override DbSet<Assento> ObterRegistro()
    {
        return _dbContext.Assentos;
    }
}
