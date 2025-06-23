using ControleDeCinema.Domínio;
using ControleDeCinema.Integracao.Compartilhado;
using FizzWare.NBuilder;

namespace ControleDeCinema.Integracao.Modulo_Sala;

[TestClass]
[TestCategory("Testes de integração de sala")]
public class RepositorioSalaEmOrmTests : RepositorioEmOrmBaseTest
{

    [TestMethod]
    public void Deve_Inserir_Sala()
    {
        Sala sala = Builder<Sala>
                .CreateNew()
                .With(x => x.Id = 0)
                .With(x => x.EmpresaId = usuarioAutenticado.Id)
                .Persist();

        var salaSelecionada = _repositorioSala.SelecionarPorId(sala.Id);

        Assert.IsNotNull(salaSelecionada);
        Assert.AreSame(sala, salaSelecionada);
    }

    [TestMethod]
    public void Deve_Editar_Sala()
    {
        Sala sala = Builder<Sala>
            .CreateNew()
            .With(x => x.Id = 0)
            .With(x => x.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        sala.Nome = "Teste de edição";

        var salaSelecionada = _repositorioSala.SelecionarPorId(sala.Id);

        Assert.IsNotNull(salaSelecionada);
        Assert.AreEqual(sala, salaSelecionada);
    }

    [TestMethod]
    public void Deve_Excluir_Sala()
    {
        Sala sala = Builder<Sala>
            .CreateNew()
            .With(x=>x.Id = 0)
            .With(x=>x.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        _repositorioSala.Excluir(sala);

        var salaSelecionada = _repositorioSala.SelecionarPorId(sala.Id);

        var salas = _repositorioSala.SelecionarTodos();

        Assert.IsNull(salaSelecionada);
        Assert.AreEqual(0, salas.Count);
    }
}
