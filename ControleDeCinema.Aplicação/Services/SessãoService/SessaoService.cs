using ControleDeCinema.Domínio;
using ControleDeCinema.Domínio.Módulo_Sessão;
using FluentResults;

namespace ControleDeCinema.Aplicação.Services.SessãoService;
public class SessaoService
{
    readonly IRepositorioSessao _repositorioSessao;
    readonly IRepositorioFilme _repositorioFilme;
    readonly IRepositorioSala _repositorioSala;

    public SessaoService(
        IRepositorioSessao repositorioSessao,
        IRepositorioFilme repositorioFilme,
        IRepositorioSala repositorioSala)
    {
        _repositorioSessao = repositorioSessao;
        _repositorioFilme = repositorioFilme;
        _repositorioSala = repositorioSala;
    }
    public Result<List<Sessão>> SelecionarTodos(int empresaId)
    {
        var sessaos = _repositorioSessao.Filtrar(x=>x.EmpresaId == empresaId);

        if (sessaos is null)
            return Result.Fail("Nenhuma sessão encontrada.");

        return Result.Ok(sessaos);
    }
    public Result<Sessão> SelecionarPorId(int id)
    {
        var sessao = _repositorioSessao.SelecionarPorId(id);

        if (sessao is null)
            return Result.Fail("Sessão não encontrada.");

        return Result.Ok(sessao);
    }

    public Result<Sessão> Cadastrar(Sessão sessao)
    {
        var Sala = _repositorioSala.SelecionarPorId(sessao.SalaId);

        if (Sala is null)
            return Result.Fail("Sala não encontrada.");

        var Filme = _repositorioFilme.SelecionarPorId(sessao.FilmeId);

        if (Filme is null)
            return Result.Fail("Filme não encontrado");

        sessao.Sala = Sala;
        sessao.Filme = Filme;
        sessao.Assentos = sessao.GerarAssentos();
        sessao.Sala.Ocupar();

        #region Erros

        if (sessao.Sala is null)
            return Result.Fail("A sessão deve conter um sala.");

        if (sessao.Filme is null)
            return Result.Fail("A sessao deve conter um filme");

        if (sessao.DataDeExibicao < DateTime.Today)
            return Result.Fail("A sessão precisa iniciar em uma data futura");
        
        if (sessao.DataDeExibicao == DateTime.Today && sessao.HorarioDaSessao < DateTime.Now.TimeOfDay)
            return Result.Fail("A sessão precisa iniciar em um horário futuro");

        #endregion

        _repositorioSessao.Inserir(sessao);

        return Result.Ok(sessao);
    }
    public Result<Sessão> Editar(Sessão sessao)
    {
        var Sala = _repositorioSala.SelecionarPorId(sessao.SalaId);

        if (Sala is null)
            return Result.Fail("Sala não encontrada.");

        var Filme = _repositorioFilme.SelecionarPorId(sessao.FilmeId);

        if (Filme is null)
            return Result.Fail("Filme não encontrado");

        sessao.Sala = Sala;
        sessao.Filme = Filme;
        sessao.Assentos.Clear();
        sessao.Sala.Ocupar();

        sessao.Assentos = sessao.GerarAssentos();
        #region Erros

        if (sessao.DataDeExibicao < DateTime.Now)
            return Result.Fail("A sessão precisa iniciar em uma data futura!");

        #endregion

        _repositorioSessao.Editar(sessao);

        return Result.Ok(sessao);
    }

    public Result<Sessão> ConfirmarVenda(Sessão sessao, List<int> selecionados)
    {
        foreach (var assento in sessao.Assentos)
        {
            if (selecionados.Contains(assento.Id))

                assento.OcuparAssento();
        }

        _repositorioSessao.Editar(sessao);

        return Result.Ok(sessao);
    }

    public Result<Sessão> Encerrar(int id)
    {
        var sessao = _repositorioSessao.SelecionarPorId(id);

        if (sessao is null)
            return Result.Fail("Sessão não encontrada.");

        sessao.Encerrar();
        //sessao.Sala.Desocupar();

        _repositorioSessao.Editar(sessao);

        return Result.Ok(sessao);
    }

}
