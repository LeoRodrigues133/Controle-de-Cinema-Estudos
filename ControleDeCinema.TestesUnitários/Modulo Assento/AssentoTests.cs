using ControleDeCinema.Domínio;

namespace ControleDeCinema.TestesUnitários;

[TestClass]
[TestCategory("Testes unitários de assento")]
public class AssentoTests
{
    [TestMethod]
    public void Deve_Criar_Assento_Valido()
    {
        var assento = new Assento("A - 1", 1);

        var erros = assento.Validar();

        Assert.AreEqual(0, erros.Count);
    }

    [TestMethod]
    public void Deve_Retornar_Erro_Se_Numero_For_Vazio()
    {
        var assento = new Assento("", 1);

        var erros = assento.Validar();

        CollectionAssert.Contains(erros, "O número do assento é obrigatório.");
    }

    [TestMethod]
    public void Deve_Retornar_Erro_Se_SessaoId_For_Invalido()
    {
        var assento = new Assento("B - 2", 0);

        var erros = assento.Validar();

        CollectionAssert.Contains(erros, "Sessão inválida para o assento.");
    }

    [TestMethod]
    public void Deve_Retornar_Todos_Erros_Quando_Tudo_For_Invalido()
    {
        var assento = new Assento("", 0);

        var erros = assento.Validar();

        List<string> errosEsperados = new()
            {
                "O número do assento é obrigatório.",
                "Sessão inválida para o assento."
            };

        CollectionAssert.AreEqual(errosEsperados, erros);
    }

    [TestMethod]
    public void Deve_Ocupar_Assento()
    {
        var assento = new Assento("C - 3", 1);

        Assert.IsFalse(assento.Disponivel);

        assento.OcuparAssento();

        Assert.IsTrue(assento.Disponivel);
    }
}