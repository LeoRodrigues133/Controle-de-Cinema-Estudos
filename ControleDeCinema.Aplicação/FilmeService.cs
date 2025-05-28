using ControleDeCinema.Domínio;
using FluentResults;

namespace ControleDeCinema.Aplicação;
public class FilmeService
{
    readonly IRepositorioFilme _repositorioFilme;

    public FilmeService(IRepositorioFilme repositorioFilme)
    {
        _repositorioFilme = repositorioFilme;
    }

    public Result<List<Filme>> SelecionarTodos()
    {
        var filmes = _repositorioFilme.SelecionarTodos();

        return Result.Ok(filmes);
    }

    public Result<Filme> SelecionarPorId(int id)
    {
        var filme = _repositorioFilme.SelecionarPorId(id);

        if (filme == null)
            Result.Fail($"Filme não encontrado!");

        return Result.Ok(filme);
    }

    public Result<Filme> Cadastrar(Filme filme)
    {
        #region Erros
        if (string.IsNullOrEmpty(filme.Nome))
            Result.Fail($"Nome inválido.");

        if (filme.Duracao < 1)
            Result.Fail($"Duração inválido.");
        #endregion

        _repositorioFilme.Inserir(filme);

        return Result.Ok(filme);
    }

    public Result<Filme> Editar(Filme filme)
    {
        #region Erros
        if (string.IsNullOrEmpty(filme.Nome))
            Result.Fail($"Nome inválido.");

        if (filme.Duracao < 1)
            Result.Fail($"Duração inválido.");
        #endregion

        _repositorioFilme.Editar(filme);

        return Result.Ok(filme);
    }

    public Result<Filme> Excluir(int id)
    {
        var filme = _repositorioFilme.SelecionarPorId(id);

        if (filme == null)
            Result.Fail("Registro não encontrado.");

        _repositorioFilme.Excluir(filme);

        return Result.Ok(filme);
    }
}
