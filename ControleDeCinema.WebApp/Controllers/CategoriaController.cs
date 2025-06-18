using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ControleDeCinema.Domínio;
using ControleDeCinema.WebApp.Models;
using ControleDeCinema.WebApp.Controllers.Compartilhado;
using ControleDeCinema.Aplicação.Services.CategoriaService;
using ControleDeCinema.Aplicação.Services.AutenticaçãoService;
using Microsoft.AspNetCore.Authorization;

namespace ControleDeCinema.WebApp.Controllers;

[Authorize(Roles = "Empresa")]
public class CategoriaController : WebController
{
    readonly CategoriaService _categoriaService;
    readonly IMapper _mapper;

    public CategoriaController(CategoriaService categoriaService, IMapper mapper, AutenticacaoService authService) : base(authService)
    {
        _categoriaService = categoriaService;
        _mapper = mapper;
    }

    public IActionResult Listar()
    {
        var resultado = _categoriaService.SelecionarTodos(EmpresaId.GetValueOrDefault());

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction("Index", "Home");
        }

        var categorias = resultado.Value;

        var categoriaVM = _mapper.Map<IEnumerable<ListarCategoriaViewModel>>(categorias);

        return View(categoriaVM);
    }
    public IActionResult Detalhes(int id)
    {
        var resultado = _categoriaService.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        var categoria = resultado.Value;

        if (categoria is null)
            return MensagemRegistroNaoEncontrado(id);

        var detalhesVm = _mapper.Map<DetalhesCategoriaViewModel>(categoria);

        return View(detalhesVm);
    }

    public IActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Cadastrar(CadastrarCategoriaViewModel categoriaVM)
    {
        if (!ModelState.IsValid)
            return View(categoriaVM);

        var categoria = _mapper.Map<Categoria>(categoriaVM);

        var resultado = _categoriaService.Cadastrar(categoria);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return View(categoriaVM);
        }

        ApresentarMensagemSucesso($"A Categoria ID [{categoria.Id}] foi cadastrada com sucesso!");

        return RedirectToAction(nameof(Listar));
    }

    public IActionResult Editar(int id)
    {
        var resultado = _categoriaService.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction("Index", "Home");
        }

        var categoria = resultado.Value;

        if (categoria is null)
            MensagemRegistroNaoEncontrado(id);

        var categoriaVM = _mapper.Map<EditarCategoriaViewModel>(categoria);

        return View(categoriaVM);
    }

    [HttpPost]
    public IActionResult Editar(EditarCategoriaViewModel categoriaVm)
    {
        if (!ModelState.IsValid)
            return View(categoriaVm);

        var categoria = _mapper.Map<Categoria>(categoriaVm);

        var resultado = _categoriaService.Editar(categoria);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return View(categoriaVm);
        }

        ApresentarMensagemSucesso($"A Categoria ID [{categoria.Id}] foi editada com sucesso!");

        return RedirectToAction(nameof(Listar));
    }

    public IActionResult Excluir(int id)
    {
        var resultado = _categoriaService.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction("Index", "Home");
        }

        var categoria = resultado.Value;

        if (categoria is null)
            MensagemRegistroNaoEncontrado(id);

        var categoriaVM = _mapper.Map<ExcluirCategoriaViewModel>(categoria);

        return View(categoriaVM);
    }

    [HttpPost]
    public IActionResult Excluir(ExcluirCategoriaViewModel excluirVm)
    {
        var resultado = _categoriaService.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        ApresentarMensagemSucesso($"A categoria foi excluída com sucesso!");

        return RedirectToAction(nameof(Listar));
    }
}