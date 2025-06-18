using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ControleDeCinema.Domínio;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using ControleDeCinema.Aplicação.Services.SalaService;
using ControleDeCinema.WebApp.Controllers.Compartilhado;
using ControleDeCinema.Aplicação.Services.AutenticaçãoService;

namespace ControleDeCinema.WebApp.Controllers;

[Authorize(Roles = "Empresa")]
public class SalaController : WebController
{
    readonly SalaService _salaService;
    readonly IMapper _mapeador;

    public SalaController(SalaService salaService, IMapper mapeador, AutenticacaoService authService) : base(authService)
    {
        _salaService = salaService;
        _mapeador = mapeador;
    }

    public IActionResult Listar()
    {
        var resultado = _salaService.SelecionarTodos(EmpresaId.GetValueOrDefault());

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction("Index", "Home");
        }

        var salas = resultado.Value;

        var salasViewModel = _mapeador.Map<IEnumerable<ListarSalaViewModel>>(salas);

        return View(salasViewModel);
    }

    public IActionResult Detalhes(int id)
    {
        var resultado = _salaService.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        var sala = resultado.Value;

        if (sala is null)
            return MensagemRegistroNaoEncontrado(id);

        var detalhesVm = _mapeador.Map<DetalhesSalaViewModel>(sala);

        return View(detalhesVm);
    }

    public IActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Cadastrar(CadastrarSalaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        var sala = _mapeador.Map<Sala>(cadastrarVm);

        var resultado = _salaService.Cadastrar(sala);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        ApresentarMensagemSucesso($"A Sala ID [{sala.Id}] foi cadastrada com sucesso!");

        return RedirectToAction(nameof(Listar));
    }

    public IActionResult Editar(int id)
    {
        var resultado = _salaService.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        var sala = resultado.Value;

        if (sala is null)
            return MensagemRegistroNaoEncontrado(id);

        var editarVm = _mapeador.Map<EditarSalaViewModel>(sala);

        return View(editarVm);
    }

    [HttpPost]
    public IActionResult Editar(EditarSalaViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        var sala = _mapeador.Map<Sala>(editarVm);

        var resultado = _salaService.Editar(sala);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        return RedirectToAction(nameof(Listar));
    }

    public IActionResult Excluir(int id)
    {
        var resultado = _salaService.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        var sala = resultado.Value;

        if (sala is null)
            return MensagemRegistroNaoEncontrado(id);

        var excluirVm = _mapeador.Map<ExcluirSalaViewModel>(sala);

        return View(excluirVm);
    }

    [HttpPost]
    public IActionResult Excluir(ExcluirSalaViewModel excluirVm)
    {
        var resultado = _salaService.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        ApresentarMensagemSucesso($"A Sala ID [{excluirVm.Id}] foi excluída com sucesso!");

        return RedirectToAction(nameof(Listar));
    }
}
