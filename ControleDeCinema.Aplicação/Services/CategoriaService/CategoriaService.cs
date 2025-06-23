using ControleDeCinema.Domínio;
using FluentResults;

namespace ControleDeCinema.Aplicação.Services.CategoriaService;
public class CategoriaService
{
    readonly IRepositorioCategoria _repositorioCategoria;

    public CategoriaService(IRepositorioCategoria repositorioCategoria)
    {
        _repositorioCategoria = repositorioCategoria;
    }

    public Result<Categoria> Cadastrar(Categoria categoria)
    {
        var erros = categoria.Validar();

        if(erros.Count > 0)
            return Result.Fail(erros);

        _repositorioCategoria.Inserir(categoria);

        return Result.Ok(categoria);
    }

    public Result<Categoria> Editar(Categoria categoria)
    {
        var erros = categoria.Validar();

        if (erros.Count > 0)
            return Result.Fail(erros);

        _repositorioCategoria.Editar(categoria);

        return Result.Ok(categoria);
    }

    public Result<Categoria> Excluir(int id)
    {
        var categoria = _repositorioCategoria.SelecionarPorId(id);

        if (categoria is null)
            return Result.Fail("Registro não encontrado.");

        _repositorioCategoria.Excluir(categoria);

        return Result.Ok(categoria);
    }

    public Result<Categoria> SelecionarPorId(int id)
    {
        var categoria = _repositorioCategoria.SelecionarPorId(id);

        if (categoria is null)
            return Result.Fail("Categoria não encontrada.");

        return Result.Ok(categoria);
    }

    public Result<List<Categoria>> SelecionarTodos(int empresaId)
    {
        var categorias = _repositorioCategoria.Filtrar(x => x.EmpresaId == empresaId);

        if (categorias is null)
            return Result.Fail("Nenhuma categoria encontrada.");

        return Result.Ok(categorias);
    }
}
