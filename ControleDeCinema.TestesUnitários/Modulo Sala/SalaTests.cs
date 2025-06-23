namespace ControleDeCinema.TestesUnitários;

using ControleDeCinema.Domínio;
using System.Collections.Generic;

[TestClass]
[TestCategory("Testes unitários de sala")]
public class SalaTests
{
    [TestMethod]
    public void Deve_Criar_Sala_Valida()
    {
        var sala = new Sala(50, "Sala 1");

        var erros = sala.Validar();

        Assert.AreEqual(0, erros.Count);
    }

    [TestMethod]
    public void Deve_Retornar_Erro_Se_Nome_For_Vazio()
    {
        var sala = new Sala(50, "");

        var erros = sala.Validar();

        CollectionAssert.AreEqual(new List<string> { "Nome inválido." }, erros);
    }

    [TestMethod]
    public void Deve_Retornar_Erro_Se_Capacidade_For_Menor_Que_1()
    {
        var sala = new Sala(0, "Sala A");

        var erros = sala.Validar();

        CollectionAssert.AreEqual(new List<string> { "Capacidade inválido." }, erros);
    }

    [TestMethod]
    public void Deve_Retornar_Erros_Se_Todos_Campos_Estiverem_Invalidos()
    {
        var sala = new Sala(0, "");

        var erros = sala.Validar();

        CollectionAssert.AreEqual(new List<string>
            {
                "Nome inválido.",
                "Capacidade inválido."
            }, erros);
    }
}