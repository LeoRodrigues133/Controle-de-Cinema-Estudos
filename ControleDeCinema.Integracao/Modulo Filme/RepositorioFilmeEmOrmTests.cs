using ControleDeCinema.Domínio;
using ControleDeCinema.Integracao.Compartilhado;
using FizzWare.NBuilder;

namespace ControleDeCinema.Integracao.Modulo_Filme;

[TestClass]
[TestCategory("Testes de integração de filme")]
public class RepositorioFilmeEmOrmTests : RepositorioEmOrmBaseTest
{
    [TestMethod]
    public void Deve_Inserir_Filme()
    {
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

        var filmeSelecionado = _repositorioFilme.SelecionarPorId(filme.Id);

        Assert.IsNotNull(filmeSelecionado);
        Assert.AreSame(filme, filmeSelecionado);
    }

    [TestMethod]
    public void Deve_Editar_Filme()
    {
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

        filme.Nome = "Teste de edição";

        var filmeSelecionado = _repositorioFilme.SelecionarPorId(filme.Id);


        Assert.IsNotNull(filmeSelecionado);
        Assert.AreEqual(filme, filmeSelecionado);
    }

    [TestMethod]
    public void Deve_Excluir_Filme()
    {
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

        _repositorioFilme.Excluir(filme);

        var filmeSelecionado = _repositorioFilme.SelecionarPorId(filme.Id);

        var filmes = _repositorioFilme.SelecionarTodos();

        Assert.IsNull(filmeSelecionado);
        Assert.AreEqual(0, filmes.Count);
    }
}
