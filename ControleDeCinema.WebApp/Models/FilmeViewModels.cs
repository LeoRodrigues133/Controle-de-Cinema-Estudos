using ControleDeCinema.Domínio;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ControleDeCinema.WebApp.Models;

public class FilmeFormViewModel
{
    public IEnumerable<SelectListItem>? Categorias { get; set; }

    [Required(ErrorMessage = "O nome do filme é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "A duração do filme é obrigatória.")]
    [Range(1, 500, ErrorMessage = "A duração deve estar entre 1 e 500 minutos.")]
    public int Duracao { get; set; }

    [Required(ErrorMessage = "A categoria é obrigatória.")]
    [Display(Name = "Categoria")]
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
    [Required]
    public int Id { get; set; }

    public Categoria Categoria { get; set; }
}

public class ExcluirFilmeViewModel
{
    [Required]
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
