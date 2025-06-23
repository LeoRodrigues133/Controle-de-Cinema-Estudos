using ControleDeCinema.Domínio;
using ControleDeCinema.Integracao.Compartilhado;
using FizzWare.NBuilder;

namespace ControleDeCinema.Integracao.Modulo_Sessao;

[TestClass]
[TestCategory("Testes de integração de sessao")]
public class RepositorioSessaoEmOrmTests : RepositorioEmOrmBaseTest
{

    [TestMethod]
    public void Deve_Inserir_Sessao()
    {
        Sala sala = Builder<Sala>
            .CreateNew()
            .With(x => x.Id = 0)
            .With(x => x.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        Categoria categoria = Builder<Categoria>
            .CreateNew()
            .With(x => x.Id = 0)
            .With(x => x.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        Filme filme = Builder<Filme>
            .CreateNew()
            .With(x => x.Id = 0)
            .With(x => x.CategoriaId = categoria.Id)
            .With(x => x.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        Sessão sessao = Builder<Sessão>
            .CreateNew()
            .With(x => x.Id = 0)
            .With(x => x.SalaId = sala.Id)
            .With(x => x.FilmeId = filme.Id)
            .With(x => x.DataDeExibicao = DateTime.Now)
            .With(x => x.HorarioDaSessao = DateTime.Now.AddHours(1).TimeOfDay)
            .With(x => x.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        var sessaoSelecionada = _repositorioSessao.SelecionarPorId(sessao.Id);

        Assert.IsNotNull(sessaoSelecionada);
        Assert.AreSame(sessao, sessaoSelecionada);
    }

    [TestMethod]
    public void Deve_Editar_Sessao()
    {
        Sala sala = Builder<Sala>
            .CreateNew()
            .With(x => x.Id = 0)
            .With(x => x.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        Categoria categoria = Builder<Categoria>
            .CreateNew()
            .With(x => x.Id = 0)
            .With(x => x.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        Filme filme = Builder<Filme>
            .CreateNew()
            .With(x => x.Id = 0)
            .With(x => x.CategoriaId = categoria.Id)
            .With(x => x.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        Sessão sessao = Builder<Sessão>
            .CreateNew()
            .With(x => x.Id = 0)
            .With(x => x.SalaId = sala.Id)
            .With(x => x.FilmeId = filme.Id)
            .With(x => x.DataDeExibicao = DateTime.Now)
            .With(x => x.HorarioDaSessao = DateTime.Now.AddHours(1).TimeOfDay)
            .With(x => x.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        sessao.HorarioDaSessao = DateTime.Now.AddHours(2).TimeOfDay;

        var sessaoSelecionada = _repositorioSessao.SelecionarPorId(sessao.Id);

        Assert.IsNotNull(sessaoSelecionada);
        Assert.AreEqual(sessao, sessaoSelecionada);
    }


    [TestMethod]
    public void Deve_Excluir_Sessao()
    {
        Sala sala = Builder<Sala>
            .CreateNew()
            .With(x => x.Id = 0)
            .With(x => x.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        Categoria categoria = Builder<Categoria>
            .CreateNew()
            .With(x => x.Id = 0)
            .With(x => x.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        Filme filme = Builder<Filme>
            .CreateNew()
            .With(x => x.Id = 0)
            .With(x => x.CategoriaId = categoria.Id)
            .With(x => x.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        Sessão sessao = Builder<Sessão>
            .CreateNew()
            .With(x => x.Id = 0)
            .With(x => x.SalaId = sala.Id)
            .With(x => x.FilmeId = filme.Id)
            .With(x => x.DataDeExibicao = DateTime.Now)
            .With(x => x.HorarioDaSessao = DateTime.Now.AddHours(1).TimeOfDay)
            .With(x => x.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        _repositorioSessao.Excluir(sessao);

        var sessaoSelecionada = _repositorioSessao.SelecionarPorId(sessao.Id);

        var sessaos = _repositorioSessao.SelecionarTodos();

        Assert.IsNull(sessaoSelecionada);
        Assert.AreEqual(0, sessaos.Count);

    }
}
