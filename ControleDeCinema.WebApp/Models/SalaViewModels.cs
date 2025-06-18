using System.ComponentModel.DataAnnotations;

namespace ControleDeCinema.WebApp.Models;

public class ListarSalaViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Capacidade { get; set; }
    // public bool Disponivel { get; set; }
}

public class CadastrarSalaViewModel
{
    [Required(ErrorMessage = "O nome da sala é obrigatório.")]
    [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "A capacidade da sala é obrigatória.")]
    [Range(1, 500, ErrorMessage = "A capacidade deve ser entre 1 e 500.")]
    public int Capacidade { get; set; }
}

public class ExcluirSalaViewModel
{
    [Required]
    public int Id { get; set; }

    public string Nome { get; set; }
    public int Capacidade { get; set; }
}

public class EditarSalaViewModel
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome da sala é obrigatório.")]
    [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "A capacidade da sala é obrigatória.")]
    [Range(1, 500, ErrorMessage = "A capacidade deve ser entre 1 e 500.")]
    public int Capacidade { get; set; }
}

public class DetalhesSalaViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Capacidade { get; set; }
}
