using AutoMapper;
using ControleDeCinema.Aplicação;
using ControleDeCinema.Domínio;
using ControleDeCinema.WebApp.Controllers.Compartilhado;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeCinema.WebApp.Controllers;
public class FilmeController : WebController
{
    readonly FilmeService _filmeService;
    readonly CategoriaService _categoriaService;
    readonly IMapper _mapper;

    public FilmeController(FilmeService filmeService, CategoriaService categoriaService, IMapper mapper)
    {
        _filmeService = filmeService;
        _categoriaService = categoriaService;
        _mapper = mapper;
    }

    public IActionResult Listar()
    {
        var resultado = _filmeService.SelecionarTodos();

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

        if (filme == null)
            MensagemRegistroNaoEncontrado(id);

        var detalhesVm = _mapper.Map<DetalhesFilmeViewModel>(filme);

        return View(detalhesVm);
    }

    public IActionResult Cadastrar()
    {


        return View(CarregarDados());
    }


    [HttpPost]
    public IActionResult Cadastrar(CadastrarFilmeViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        var filme = _mapper.Map<Filme>(cadastrarVm);

        var resultado = _filmeService.Cadastrar(filme);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return View(CarregarDados(cadastrarVm));
        }

        ApresentarMensagemSucesso($"O filme ID [{filme.Id}] foi cadastrado com sucesso!");

        return View("Home", "Index");
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

        var editarVm = _mapper.Map<EditarFilmeViewModel>(filme);

        return View(CarregarDados(editarVm));
    }

    [HttpPost]
    public IActionResult Editar(EditarFilmeViewModel editarVm)
    {

        if (!ModelState.IsValid)
            return View(editarVm);

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

        var excluirVm = _mapper.Map<ExcluirFilmeViewModel>(filme);

        return View(excluirVm);
    }

    [HttpPost]
    public IActionResult Excluir(ExcluirFilmeViewModel excluirVm)
    {
        if (!ModelState.IsValid)
            return View(excluirVm);

        var resultado = _filmeService.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        ApresentarMensagemSucesso($"O filme foi excluído com sucesso!");

        return RedirectToAction(nameof(Listar));
    }

    private SelectListItemViewModel? CarregarDados(SelectListItemViewModel? Dados = null)
    {
        var resultado = _categoriaService.SelecionarTodos();

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return null;
        }

        var categorias = resultado.Value;

        if (Dados is null)
            Dados = new SelectListItemViewModel();


        Dados.Categorias = categorias.Select(c => new SelectListItem(c.Nome, c.Id.ToString()));

        return Dados;
    }
}
