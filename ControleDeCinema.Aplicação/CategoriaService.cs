using ControleDeCinema.Domínio;
using FluentResults;

namespace ControleDeCinema.Aplicação;
public class CategoriaService
{
    readonly IRepositorioCategoria _repositorioCategoria;

    public CategoriaService(IRepositorioCategoria repositorioCategoria)
    {
        _repositorioCategoria = repositorioCategoria;
    }

    public Result<Categoria> Cadastrar(Categoria categoria)
    {
        #region Erros
        if (string.IsNullOrEmpty(categoria.Nome) || categoria.Nome.Length < 1)
            return Result.Fail("Nome inválido.");
        #endregion

        _repositorioCategoria.Inserir(categoria);

        return Result.Ok(categoria);
    }

    public Result<Categoria> Editar(Categoria categoria)
    {
        #region Erros
        if (string.IsNullOrEmpty(categoria.Nome) || categoria.Nome.Length < 1)
            return Result.Fail("Nome inválido.");
        #endregion

        _repositorioCategoria.Editar(categoria);

        return Result.Ok(categoria);
    }

    public Result<Categoria> Excluir(int id)
    {
        var categoria = _repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return Result.Fail("Registro não encontrado.");

        _repositorioCategoria.Excluir(categoria);

        return Result.Ok(categoria);
    }

    public Result<Categoria> SelecionarPorId(int id)
    {
        var categoria = _repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return Result.Fail("Categoria não encontrada.");

        return Result.Ok(categoria);
    }

    public Result<List<Categoria>> SelecionarTodos()
    {
        var categorias = _repositorioCategoria.SelecionarTodos();

        if (categorias == null)
            return Result.Fail("Nenhuma categoria encontrada.");

        return Result.Ok(categorias);
    }
}
