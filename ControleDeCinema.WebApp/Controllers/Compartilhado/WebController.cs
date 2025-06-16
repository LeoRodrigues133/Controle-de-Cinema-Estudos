using FluentResults;
using ControleDeCinema.WebApp.Extensions;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using ControleDeCinema.Aplicação.AutenticaçãoService;

namespace ControleDeCinema.WebApp.Controllers.Compartilhado;

public class WebController : Controller
{
    protected readonly AutenticacaoService _authService;

    protected WebController(AutenticacaoService authService)
    {
        _authService = authService;
    }

    protected int? UsuarioId
    {
        get
        {
            var usuarioId = _authService.ObterIdUsuarioAsync(User).Result;

            return usuarioId;
        }
    }
    protected IActionResult MensagemRegistroNaoEncontrado(int idRegistro)
    {
        TempData.SerializarMensagemViewModel(new MensagemViewModel
        {
            Titulo = "Registro não encontrado",
            Mensagem = $"O registro com o id {idRegistro} não foi encontrado.",
        });

        return RedirectToAction("Index", "Home");
    }

    protected void ApresentarMensagemFalha(Result resultado)
    {
        ViewBag.Mensagem = new MensagemViewModel
        {
            Titulo = "Falha",
            Mensagem = resultado.Errors[0].Message,
        };
    }

    protected void ApresentarMensagemSucesso(string mensagem)
    {
        ViewBag.Mensagem = new MensagemViewModel
        {
            Titulo = "Sucesso",
            Mensagem = mensagem,
        };
    }

    protected void ApresentarMensagemFalhaEditavel(string mensagem)
    {
        ViewBag.Mensagem = new MensagemViewModel
        {
            Titulo = "Erro",
            Mensagem = mensagem,
        };
    }
}
