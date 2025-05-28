using ControleDeCinema.Domínio;
using ControleDeCinema.Infra.Compartilhado;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeCinema.Infra.Módulo_Filme;
public class RepositorioFilmeEmOrm : RepositorioBaseEmOrm<Filme>, IRepositorioFilme
{

    public RepositorioFilmeEmOrm(CinemaDbContext dbContext) : base (dbContext)
    {
    }

    public List<Filme> Filtrar(Func<Filme, bool> predicate)
    {
        throw new NotImplementedException();
    }

    protected override DbSet<Filme> ObterRegistro()
    {
        return _dbContext.Filmes;
    }
}
