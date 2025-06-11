using ControleDeCinema.Domínio;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeCinema.WebApp.Models;

public class FilmeFormViewModel
{
    public IEnumerable<SelectListItem>? Categorias { get; set; }
    public string Nome { get; set; }
    public int Duracao { get; set; }
    public int CategoriaId { get; set; }

}
public class ListarFilmeViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Duracao { get; set; }
    public Categoria Categoria { get; set; }
}

public class EditarFilmeViewModel : FilmeFormViewModel
{
    public int Id { get; set; }
    public Categoria Categoria { get; set; }

}

public class ExcluirFilmeViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Duracao { get; set; }
    public Categoria Categoria { get; set; }
}
public class DetalhesFilmeViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Duracao { get; set; }
    public Categoria Categoria { get; set; }
}

public class CadastrarFilmeViewModel : FilmeFormViewModel
{

}
