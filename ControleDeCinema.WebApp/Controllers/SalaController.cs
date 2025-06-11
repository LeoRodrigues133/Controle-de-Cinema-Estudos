using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ControleDeCinema.Domínio;
using ControleDeCinema.Aplicação;
using ControleDeCinema.WebApp.Models;
using ControleDeCinema.WebApp.Controllers.Compartilhado;

namespace ControleDeCinema.WebApp.Controllers;
public class SalaController : WebController
{
    readonly SalaService _salaService;
    readonly IMapper _mapeador;

    public SalaController(SalaService salaService, IMapper mapeador)
    {
        _salaService = salaService;
        _mapeador = mapeador;
    }

    public IActionResult Listar()
    {
        var resultado = _salaService.SelecionarTodos();

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
    //[ValidateAntiForgeryToken]
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
    //[ValidateAntiForgeryToken]
    public IActionResult Editar(EditarSalaViewModel editarVm)
    {
        if(!ModelState.IsValid)
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
    //[ValidateAntiForgeryToken]
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
