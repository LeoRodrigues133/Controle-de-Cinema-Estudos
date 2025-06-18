using System.ComponentModel.DataAnnotations;

namespace ControleDeCinema.WebApp.Models;

public class ListarCategoriaViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres.")]
    public string Nome { get; set; }
}

public class CadastrarCategoriaViewModel
{
    [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres.")]
    public string Nome { get; set; }
}

public class ExcluirCategoriaViewModel
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    public string Nome { get; set; }
}

public class EditarCategoriaViewModel
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres.")]
    public string Nome { get; set; }
}

public class DetalhesCategoriaViewModel
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    public string Nome { get; set; }
}
