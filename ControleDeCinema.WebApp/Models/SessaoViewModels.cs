using ControleDeCinema.Domínio;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeCinema.WebApp.Models;

public class SessaoFormViewModel
{
    public int? Id { get; set; }
    public IEnumerable<SelectListItem>? Salas { get; set; }
    public IEnumerable<SelectListItem>? Filmes { get; set; }
    public IEnumerable<Assento>? Assentos { get; set; }
    public int FilmeId { get; set; }
    public int SalaId { get; set; }
    public DateTime DataDeExibicao { get; set; }
    public TimeSpan HorarioDaSessao { get; set; }
}
public class ListarSessaoViewModel
{
    public int Id { get; set; }
    public Filme Filme { get; set; }
    public Sala Sala { get; set; }
    public DateTime DataDeExibicao { get; set; }
    public TimeSpan HorarioDaSessao { get; set; }

    public bool Finalizada { get; set; }

}
public class CadastrarSessaoViewModel : SessaoFormViewModel
{

}

public class DetalhesSessaoViewModel : SessaoFormViewModel
{
    public int Id { get; set; }
    public bool Finalizada { get; set; }
    public Sala Sala { get; set; }
    public Filme Filme { get; set; }
}
public class EditarSessaoViewModel : SessaoFormViewModel
{
}

public class VenderSessaoViewModel 
{
    public int Id { get; set; }
    public Filme Filme { get; set; }
    public Sala Sala { get; set; }
    public DateTime DataDeExibicao { get; set; }
    public TimeSpan HorarioDaSessao { get; set; }
    public List<AssentoViewModel>? Assentos { get; set; }
}

public class AssentoViewModel
{
    public int Id { get; set; }
    public string Numero { get; set; }  // Ex: A1, B2
    public bool Disponivel { get; set; }
}
public class EncerrarSessaoViewModel
{
    public int Id { get; set; }
    public Filme Filme { get; set; }
    public Sala Sala { get; set; }
    public DateTime DataDeExibicao { get; set; }
    public TimeSpan HorarioDaSessao { get; set; }

}

