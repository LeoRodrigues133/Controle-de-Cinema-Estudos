using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ControleDeCinema.WebApp.Models;

public class RegistrarViewModel
{
    public RegistrarViewModel()
    {
        Tipos = [
            new SelectListItem { Value="Empresa", Text= "Empresa"},
            new SelectListItem { Value="Cliente", Text= "Cliente" }
            ];
    }

    [Required]
    public string? Usuario { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Senha { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirmar senha")]
    [Compare("Senha", ErrorMessage = "As senhas não conferem")]
    public string? ConfirmaSenha { get; set; }

    [Required]
    public string? Tipo { get; set; }

    [Required]
    public IEnumerable<SelectListItem> Tipos { get; set; }

}


public class LoginViewModel
{
    [Required]
    public string? Usuario { get; set; }

    [Required(ErrorMessage = "A senha é obrigatoria")]
    [DataType(DataType.Password)]
    public string? Senha { get; set; }

    [Display(Name = "Lembrar-me")]
    public bool lembrarMe { get; set; }
}