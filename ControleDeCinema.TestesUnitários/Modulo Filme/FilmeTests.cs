namespace ControleDeCinema.TestesUnitários;
using ControleDeCinema.Domínio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;


[TestClass]
[TestCategory("Testes unitários de filme")]
public class FilmeTests
{
    [TestMethod]
    public void Deve_Criar_Filme_Valido()
    {
        var categoria = new Categoria("Ação");
        var filme = new Filme("Matrix", 120, categoria.Id)
        {
            Categoria = categoria
        };

        var erros = filme.Validar();

        Assert.AreEqual(0, erros.Count);
    }

    [TestMethod]
    public void Deve_Retornar_Erro_Se_Nome_For_Vazio()
    {
        var categoria = new Categoria("Terror");

        var filme = new Filme("", 120, categoria.Id)
        {
            Categoria = categoria
        };

        var erros = filme.Validar();

        CollectionAssert.AreEqual(new List<string> { "Nome inválido." }, erros);
    }

    [TestMethod]
    public void Deve_Retornar_Erro_Se_Duracao_For_Invalida()
    {
        var categoria = new Categoria("Comédia");
        var filme = new Filme("Filme Teste", 0, categoria.Id)
        {
            Categoria = categoria
        };

        var erros = filme.Validar();

        CollectionAssert.AreEqual(new List<string> { "Duração inválido." }, erros);
    }

    [TestMethod]
    public void Deve_Retornar_Erro_Se_Categoria_For_Nula()
    {
        var filme = new Filme("Sem Categoria", 100, 1);

        var erros = filme.Validar();

        CollectionAssert.Contains(erros, "O filme deve conter uma categoria");
    }

    [TestMethod]
    public void Deve_Retornar_Todos_Erros_Se_Tudo_Estiver_Invalido()
    {
        var filme = new Filme("", 0, 0);

        var erros = filme.Validar();

        CollectionAssert.AreEquivalent(new List<string>
            {
                "O filme deve conter uma categoria",
                "Nome inválido.",
                "Duração inválido.",
                "O filme deve conter uma categoria."
            }, erros);
    }
}