using ControleDeCinema.Domínio;
using FluentResults;

namespace ControleDeCinema.Aplicação.Services.SalaService;

public class SalaService
{
    readonly IRepositorioSala _repositorioSala;

    public SalaService(IRepositorioSala repositorioSala)
    {
        _repositorioSala = repositorioSala;
    }

    public Result<Sala> Cadastrar(Sala sala)
    {
        #region Erros
        if (string.IsNullOrEmpty(sala.Nome) || sala.Nome.Length < 1)
            Result.Fail("Nome inválido.");

        if (sala.Capacidade < 1)
            Result.Fail("Capacidade inválido.");
        #endregion

        _repositorioSala.Inserir(sala);

        return Result.Ok(sala);
    }

    public Result<Sala> Editar(Sala sala)
    {
        #region Erros
        if (string.IsNullOrEmpty(sala.Nome) || sala.Nome.Length < 1)
            Result.Fail("Nome inválido.");
        if (sala.Capacidade < 1)
            Result.Fail("Capacidade inválido.");
        #endregion

        _repositorioSala.Editar(sala);

        return Result.Ok(sala);
    }
    public Result<Sala> Excluir(int id)
    {
        var sala = _repositorioSala.SelecionarPorId(id);

        if (sala == null)
            return Result.Fail("Registro não encontrado.");

        _repositorioSala.Excluir(sala);

        return Result.Ok(sala);
    }

    public Result<List<Sala>> SelecionarTodos(int empresaId)
    {
        var salas = _repositorioSala.Filtrar(x => x.EmpresaId == empresaId);

        return Result.Ok(salas);
    }
    public Result<Sala> SelecionarPorId(int id)
    {
        var sala = _repositorioSala.SelecionarPorId(id);

        if (sala == null)
            return Result.Fail("Sala não encontrada.");

        return Result.Ok(sala);
    }
}
