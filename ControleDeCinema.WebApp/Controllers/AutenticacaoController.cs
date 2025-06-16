using ControleDeCinema.Aplicação.AutenticaçãoService;
using ControleDeCinema.Domínio;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeCinema.WebApp.Controllers;
public class AutenticacaoController : Controller
{
    readonly AutenticacaoService _autenticacaoService;

    public AutenticacaoController(AutenticacaoService autenticacaoService)
    {
        _autenticacaoService = autenticacaoService;
    }

    public IActionResult Registrar()
    {
        return View(new RegistrarViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> RegistrarAsync(RegistrarViewModel RegistroVm)
    {
        if (!ModelState.IsValid)
            return View(RegistroVm);

        var usuario = new Usuario()
        {
            UserName = RegistroVm.Usuario,
            Email = RegistroVm.Email
        };

        var senha = RegistroVm.Senha!;

        var resultado = await _autenticacaoService.Registrar(usuario, senha, RegistroVm.Tipo);

        if (resultado.IsSuccess)
            return RedirectToAction("Index", "Home");

        foreach (var erro in resultado.Errors)
            ModelState.AddModelError(string.Empty, erro.Message);

        return View(RegistroVm);
    }

    public IActionResult Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginVm, string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;

        if(!ModelState.IsValid)
            return View(loginVm);

        var resultado = await _autenticacaoService.Login(loginVm.Usuario!, loginVm.Senha!);

        if (resultado.IsSuccess)
            return LocalRedirect(returnUrl ?? "/");

        var msgError = resultado.Errors.First().Message;

        ModelState.AddModelError(string.Empty,msgError);

        return View(loginVm);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _autenticacaoService.Logout();

        return RedirectToAction(nameof(Login));
    }

    public IActionResult AcessoNegado()
    {
        return View();
    }
}
