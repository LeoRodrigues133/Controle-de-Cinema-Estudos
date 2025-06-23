using ControleDeCinema.Domínio;

namespace ControleDeCinema.TestesUnitários;

[TestClass]
[TestCategory("Testes unitários de categoria")]
public class CategoriaTests
{
    [TestMethod]
    public void Deve_Criar_Categoria_Valida()
    {
        var categoria = new Categoria("Terror");

        var erros = categoria.Validar();

        Assert.AreEqual(0, erros.Count);
    }

    [TestMethod]
    public void Deve_Retornar_Erro_Se_Nome_For_Vazio()
    {
        var categoria = new Categoria("");

        var erros = categoria.Validar();

        CollectionAssert.AreEqual(new List<string> { "Nome inválido." }, erros);
    }

    [TestMethod]
    public void Deve_Retornar_Erro_Se_Nome_For_Nulo()
    {
        var categoria = new Categoria(null!);

        var erros = categoria.Validar();

        CollectionAssert.AreEqual(new List<string> { "Nome inválido." }, erros);
    }
}