using FizzWare.NBuilder;
using ControleDeCinema.Domínio;
using ControleDeCinema.Integracao.Compartilhado;

namespace ControleDeCinema.Integracao;

[TestClass]
[TestCategory("Testes de integração de categoria")]
public class RepositorioCategoriaEmOrmTests : RepositorioEmOrmBaseTest
{
    [TestMethod]
    public void Deve_Inserir_Categoria()
    {
        Categoria novaCategoria = Builder<Categoria>
            .CreateNew()
            .With(x => x.Id = 0)
            .With(x => x.EmpresaId = usuarioAutenticado.Id)
            .Build(); // Não salva diretamente no banco, necessário Utilizar o método abaixo,
                        // Não é necesssário se estiver utilizando o método implementado no Base

        _repositorioCategoria.Inserir(novaCategoria);

        Categoria categoriaSelecionada = _repositorioCategoria.SelecionarPorId(novaCategoria.Id);

        //Assert.AreEqual(novaCategoria, categoriaSelecionada); <<-- Compara valores de cada objeto -->>

        Assert.IsNotNull(categoriaSelecionada);
        Assert.AreSame(novaCategoria, categoriaSelecionada); // Compara se duas referências de objeto
                                                             // apontam para o mesmo objeto na memória.
    }

    [TestMethod]
    public void Deve_Editar_Categoria()
    {
        //Arrange
        Categoria categoria = Builder<Categoria>
            .CreateNew()
            .With(x => x.Id = 0)
            .With(x => x.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        categoria.Nome = "Teste de Edição";

        var categoriaSelecionada = _repositorioCategoria.SelecionarPorId(categoria.Id);

        Assert.IsNotNull(categoriaSelecionada);
        Assert.AreEqual(categoria, categoriaSelecionada);
    }

    [TestMethod]
    public void Deve_Excluir_Categoria()
    {
        Categoria categoria = Builder<Categoria>
            .CreateNew()
            .With(x => x.Id = 0)
            .With(x => x.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        _repositorioCategoria.Excluir(categoria);

        var categoriaSelecionada = _repositorioCategoria.SelecionarPorId(categoria.Id);
        var categorias = _repositorioCategoria.SelecionarTodos();

        Assert.IsNull(categoriaSelecionada);
        Assert.AreEqual(0, categorias.Count);
    }

}
