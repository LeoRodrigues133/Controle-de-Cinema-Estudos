using ControleDeCinema.Domínio;
using FluentResults;

namespace ControleDeCinema.Aplicação.Services.FilmeService;
public class FilmeService
{
    readonly IRepositorioFilme _repositorioFilme;
    readonly IRepositorioCategoria _repositorioCategoria;

    public FilmeService(IRepositorioFilme repositorioFilme, IRepositorioCategoria repositorioCategoria)
    {
        _repositorioFilme = repositorioFilme;
        _repositorioCategoria = repositorioCategoria;
    }

    public Result<List<Filme>> SelecionarTodos(int empresaId)
    {
        var filmes = _repositorioFilme.Filtrar(x => x.EmpresaId == empresaId);

        return Result.Ok(filmes);
    }

    public Result<Filme> SelecionarPorId(int id)
    {
        var filme = _repositorioFilme.SelecionarPorId(id);

        if (filme is null)
            return Result.Fail($"Filme não encontrado!");

        return Result.Ok(filme);
    }

    public Result<Filme> Cadastrar(Filme filme)
    {

        var categoria = _repositorioCategoria.SelecionarPorId(filme.CategoriaId);

        if (categoria is null)
            return Result.Fail("Categoria não encontrado");

        filme.Categoria = categoria;

        #region Erros
        if (filme.Categoria is null)
            return Result.Fail("O filme deve conter uma categoria");

        if (string.IsNullOrEmpty(filme.Nome))
            return Result.Fail("Nome inválido.");

        if (filme.Duracao < 1)
            return Result.Fail("Duração inválido.");

        if (filme.Categoria is null)
            return Result.Fail("O filme deve conter uma categoria.");
        #endregion


        _repositorioFilme.Inserir(filme);

        return Result.Ok(filme);
    }

    public Result<Filme> Editar(Filme filme)
    {
        #region Erros
        if (string.IsNullOrEmpty(filme.Nome))
            return Result.Fail($"Nome inválido.");

        if (filme.Duracao < 1)
            return Result.Fail($"Duração inválido.");
        #endregion

        _repositorioFilme.Editar(filme);

        return Result.Ok(filme);
    }

    public Result<Filme> Excluir(int id)
    {
        var filme = _repositorioFilme.SelecionarPorId(id);

        if (filme is null)
            return Result.Fail("Registro não encontrado.");

        _repositorioFilme.Excluir(filme);

        return Result.Ok(filme);
    }
}
