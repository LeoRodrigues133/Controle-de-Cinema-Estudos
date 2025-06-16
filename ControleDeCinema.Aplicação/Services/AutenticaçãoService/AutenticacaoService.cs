using ControleDeCinema.Domínio;
using ControleDeCinema.Domínio.Módulo_Autenticação;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace ControleDeCinema.Aplicação.Services.AutenticaçãoService;
public class AutenticacaoService
{
    readonly UserManager<Usuario> _userManager;
    readonly SignInManager<Usuario> _signInManager;
    readonly RoleManager<Perfil> _roleManager;

    public AutenticacaoService(
        UserManager<Usuario> userManager,
        SignInManager<Usuario> signInManager,
        RoleManager<Perfil> roleManager
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task<Result<Usuario>> Registrar(Usuario usuario, string senha, TipoUsuarioEnum tipoEnum)
    {
        var resultadoUsuario = await _userManager.CreateAsync(usuario, senha);

        if (!resultadoUsuario.Succeeded)
        {
            var erros = resultadoUsuario.Errors.Select(s => s.Description);
            return Result.Fail(erros);
        }

        var tipoUsuario = tipoEnum.ToString();

        var resultadoTipoUsuario = await _roleManager.FindByNameAsync(tipoUsuario);

        if (resultadoTipoUsuario is null)
        {
            var cargo = new Perfil
            {
                Name = tipoUsuario,
                NormalizedName = tipoUsuario.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            await _roleManager.CreateAsync(cargo);
        }

        await _userManager.AddToRoleAsync(usuario, tipoUsuario);

        if (tipoEnum == TipoUsuarioEnum.Empresa)

            await _signInManager.SignInAsync(usuario, isPersistent: false);

        return Result.Ok(usuario);
    }

    public async Task<Result> Login(string usuario, string senha)
    {
        var resultado = await _signInManager.PasswordSignInAsync(
            usuario,
            senha,
            false,
            false);

        if (!resultado.Succeeded)
            return Result.Fail("Login ou senha incorretos");

        return Result.Ok();
    }

    public async Task<Result> Logout()
    {
        await _signInManager.SignOutAsync();

        return Result.Ok();
    }

    public async Task<int?> ObterIdEmpresaAsync(ClaimsPrincipal claim)
    {
        var usuario = await _userManager.GetUserAsync(claim);

        return usuario?.Id;
    }
}
