using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ControleDeCinema.Domínio;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using ControleDeCinema.WebApp.Controllers.Compartilhado;
using ControleDeCinema.Aplicação.Services.FilmeService;
using ControleDeCinema.Aplicação.Services.SalaService;
using ControleDeCinema.Aplicação.Services.SessãoService;
using ControleDeCinema.Aplicação.Services.AutenticaçãoService;
using Microsoft.AspNetCore.Authorization;

namespace ControleDeCinema.WebApp.Controllers;

[Authorize(Roles = "Empresa")]
public class SessaoController : WebController
{
    readonly SessaoService _sessaoService;
    readonly FilmeService _filmeService;
    readonly SalaService _salaService;
    readonly IMapper _mapper;

    public SessaoController(
        SessaoService sessaoService,
        FilmeService filmeService,
        SalaService salaService,
        IMapper mapper,
        AutenticacaoService authService) : base(authService)
    {
        _sessaoService = sessaoService;
        _filmeService = filmeService;
        _salaService = salaService;
        _mapper = mapper;
    }

    public IActionResult Listar()
    {
        var resultado = _sessaoService.SelecionarTodos(EmpresaId.GetValueOrDefault());

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction("Index", "Home");
        }

        var sessaos = resultado.Value;

        var listarVm = _mapper.Map<IEnumerable<ListarSessaoViewModel>>(sessaos);

        return View(listarVm);
    }

    public IActionResult Detalhes(int id)
    {
        var resultado = _sessaoService.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction("Index", "Home");
        }

        var sessao = resultado.Value;

        var detalhesVm = _mapper.Map<DetalhesSessaoViewModel>(sessao);

        return View(detalhesVm);
    }

    public IActionResult Cadastrar()
    {
        return View(CarregarDados());
    }

    [HttpPost]
    public IActionResult Cadastrar(SessaoFormViewModel cadastrasVM)
     {
        if (!ModelState.IsValid)
            return View(CarregarDados(cadastrasVM));

        var sessao = _mapper.Map<Sessão>(cadastrasVM);

        var resultado = _sessaoService.Cadastrar(sessao);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return View(CarregarDados(cadastrasVM));
        }

        ApresentarMensagemSucesso($"A Sessão ID [{sessao.Id}] foi cadastrado com sucesso!");

        return RedirectToAction(nameof(Listar));
    }
    public IActionResult Editar(int id)
    {
        var resultado = _sessaoService.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return View("Home", "Index");
        }
        var sessao = resultado.Value;

        if (sessao is null)
            MensagemRegistroNaoEncontrado(id);

        var editarVm = _mapper.Map<SessaoFormViewModel>(sessao);

        return View(CarregarDados(editarVm));
    }

    [HttpPost]
    public IActionResult Editar(SessaoFormViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(CarregarDados(editarVm));

        var sessao = _mapper.Map<Sessão>(editarVm);

        var resultado = _sessaoService.Editar(sessao);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return View(CarregarDados(editarVm));
        }

        ApresentarMensagemSucesso($"A Sessão ID [{sessao.Id}] foi editada com sucesso!");

        return RedirectToAction(nameof(Listar));
    }

    public IActionResult Vender(int id)
    {
        var resultado = _sessaoService.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return View("Home", "Index");
        }


        var sessao = resultado.Value;

        if (sessao is null)
            MensagemRegistroNaoEncontrado(id);

        var vIngressosVm = _mapper.Map<VenderSessaoViewModel>(sessao);

        return View(vIngressosVm);
    }

    [HttpPost]
    public IActionResult Vender(int id, List<int> assentosSelecionados)
    {
        var resultado = _sessaoService.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        var sessao = resultado.Value;

        var resultadoEdicao = _sessaoService.ConfirmarVenda(sessao, assentosSelecionados);

        var vendaVm = _mapper.Map<VenderSessaoViewModel>(sessao);

        if (resultadoEdicao.IsFailed)
        {
            ApresentarMensagemFalha(resultadoEdicao.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        ApresentarMenssagemDeVenda(assentosSelecionados, sessao);

        return View(vendaVm);
    }

    private void ApresentarMenssagemDeVenda(List<int> assentosSelecionados, Sessão sessao)
    {
        var plural = assentosSelecionados.Count > 2 ? "s" : "";

        ApresentarMensagemSucesso($"Ingresso{plural} vendido{plural} para [{sessao.Filme?.Nome}]!");
    }

    public IActionResult Encerrar(int id)
    {
        var resultado = _sessaoService.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return View("Home", "Index");
        }

        var sessao = resultado.Value;

        if (sessao is null)
            MensagemRegistroNaoEncontrado(id);

        var finalizarVm = _mapper.Map<EncerrarSessaoViewModel>(sessao);

        return View(finalizarVm);
    }

    [HttpPost]
    public IActionResult Encerrar(EncerrarSessaoViewModel excluirVm)
    {
        var resultado = _sessaoService.Encerrar(excluirVm.Id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        ApresentarMensagemSucesso($"A Sessão foi encerrada com sucesso!");

        return RedirectToAction(nameof(Listar));
    }

    private SessaoFormViewModel? CarregarDados(SessaoFormViewModel? Dados = null)
    {
        var filmes = _filmeService.SelecionarTodos(EmpresaId.GetValueOrDefault()).Value;
        var salas = _salaService.SelecionarTodos(EmpresaId.GetValueOrDefault()).Value;


        if (Dados is null)
            Dados = new SessaoFormViewModel();


        Dados.Filmes = filmes.Select(c => new SelectListItem(c.Nome, c.Id.ToString()));

        Dados.Salas = salas.Select(c => new SelectListItem(c.Nome, c.Id.ToString()));

        return Dados;
    }
}
