using System.ComponentModel.DataAnnotations;

namespace ControleDeCinema.Domínio.Módulo_Autenticação;
public enum TipoUsuarioEnum
{
    Empresa,
    [Display(Name = "Funcionário")] Funcionario
}
