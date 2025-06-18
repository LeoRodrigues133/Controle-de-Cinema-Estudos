using AutoMapper;
using ControleDeCinema.Domínio;
using Microsoft.AspNetCore.Mvc;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using ControleDeCinema.Aplicação.Services.FilmeService;
using ControleDeCinema.WebApp.Controllers.Compartilhado;
using ControleDeCinema.Aplicação.Services.CategoriaService;
using ControleDeCinema.Aplicação.Services.AutenticaçãoService;
using Microsoft.AspNetCore.Authorization;

namespace ControleDeCinema.WebApp.Controllers;

[Authorize(Roles = "Empresa")]
public class FilmeController : WebController
{
    readonly FilmeService _filmeService;
    readonly CategoriaService _categoriaService;
    readonly IMapper _mapper;

    public FilmeController(FilmeService filmeService, CategoriaService categoriaService, IMapper mapper, AutenticacaoService authService) : base(authService)
    {
        _filmeService = filmeService;
        _categoriaService = categoriaService;
        _mapper = mapper;
    }

    public IActionResult Listar()
    {
        var resultado = _filmeService.SelecionarTodos(EmpresaId.GetValueOrDefault());

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction("Home", "Index");
        }

        var filmes = resultado.Value;

        var filmeVm = _mapper.Map<IEnumerable<ListarFilmeViewModel>>(filmes);

        return View(filmeVm);
    }

    public IActionResult Detalhes(int id)
    {
        var resultado = _filmeService.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        var filme = resultado.Value;

        if (filme is null)
            MensagemRegistroNaoEncontrado(id);

        var detalhesVm = _mapper.Map<DetalhesFilmeViewModel>(filme);

        return View(detalhesVm);
    }

    public IActionResult Cadastrar()
    {
        return View(CarregarDados());
    }


    [HttpPost]
    public IActionResult Cadastrar(FilmeFormViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(CarregarDados(cadastrarVm));

        var filme = _mapper.Map<Filme>(cadastrarVm);

        var resultado = _filmeService.Cadastrar(filme);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return View(CarregarDados(cadastrarVm));
        }

        ApresentarMensagemSucesso($"O filme ID [{filme.Id}] foi cadastrado com sucesso!");

        return RedirectToAction(nameof(Listar));
    }

    public IActionResult Editar(int id)
    {
        var resultado = _filmeService.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return View("Home", "Index");
        }

        var filme = resultado.Value;

        if (filme is null)
            MensagemRegistroNaoEncontrado(id);

        var editarVm = _mapper.Map<EditarFilmeViewModel>(filme);

        return View(CarregarDados(editarVm));
    }

    [HttpPost]
    public IActionResult Editar(FilmeFormViewModel editarVm)
    {

        if (!ModelState.IsValid)
            return View(CarregarDados(editarVm));

        var filme = _mapper.Map<Filme>(editarVm);

        var resultado = _filmeService.Editar(filme);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return View(CarregarDados(editarVm));
        }

        ApresentarMensagemSucesso($"O filme ID [{filme.Id}] foi editado com sucesso!");

        return RedirectToAction(nameof(Listar));
    }

    public IActionResult Excluir(int id)
    {
        var resultado = _filmeService.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return View("Home", "Index");
        }

        var filme = resultado.Value;

        if (filme is null)
            MensagemRegistroNaoEncontrado(id);

        var excluirVm = _mapper.Map<ExcluirFilmeViewModel>(filme);

        return View(excluirVm);
    }

    [HttpPost]
    public IActionResult Excluir(ExcluirFilmeViewModel excluirVm)
    {
        var resultado = _filmeService.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        ApresentarMensagemSucesso($"O filme foi excluído com sucesso!");

        return RedirectToAction(nameof(Listar));
    }

    private FilmeFormViewModel? CarregarDados(FilmeFormViewModel? Dados = null)
    {
        var categorias = _categoriaService.SelecionarTodos(EmpresaId.GetValueOrDefault()).Value;

        if (Dados is null)
            Dados = new FilmeFormViewModel();

        Dados.Categorias = categorias.Select(c => new SelectListItem(c.Nome, c.Id.ToString()));

        return Dados;
    }
}