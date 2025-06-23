using ControleDeCinema.Domínio;

namespace ControleDeCinema.TestesUnitários;

[TestClass]
[TestCategory("Testes unitários de sessão")]
public class SessaoTests
{
    private Sala salaValida;
    private Filme filmeValido;

    [TestInitialize]
    public void Setup()
    {
        salaValida = new Sala(50, "Sala A");
        filmeValido = new Filme("Matrix", 120, 1);
        filmeValido.Categoria = new Categoria("Ação");
    }

    [TestMethod]
    public void Deve_Criar_Sessao_Valida()
    {
        var sessao = new Sessão(
            filmeValido.Id,
            salaValida.Id,
            DateTime.Today.AddDays(1),
            new TimeSpan(18, 0, 0))
        {
            Sala = salaValida,
            Filme = filmeValido
        };

        var erros = sessao.Validar();

        Assert.AreEqual(0, erros.Count);
    }

    [TestMethod]
    public void Deve_Retornar_Erro_Se_Sala_For_Nula()
    {
        var sessao = new Sessão(
            filmeValido.Id,
            0,
            DateTime.Today.AddDays(1),
            new TimeSpan(20, 0, 0))
        {
            Filme = filmeValido
        };

        var erros = sessao.Validar();

        CollectionAssert.Contains(erros, "A sessão deve conter um sala.");
    }

    [TestMethod]
    public void Deve_Retornar_Erro_Se_Filme_For_Nulo()
    {
        var sessao = new Sessão(
            0,
            salaValida.Id,
            DateTime.Today.AddDays(1),
            new TimeSpan(20, 0, 0))
        {
            Sala = salaValida
        };

        var erros = sessao.Validar();

        CollectionAssert.Contains(erros, "A sessao deve conter um filme");
    }

    [TestMethod]
    public void Deve_Retornar_Erro_Se_Data_For_Passada()
    {
        var sessao = new Sessão(
            filmeValido.Id,
            salaValida.Id,
            DateTime.Today.AddDays(-1),
            new TimeSpan(20, 0, 0))
        {
            Sala = salaValida,
            Filme = filmeValido
        };

        var erros = sessao.Validar();

        CollectionAssert.Contains(erros, "A sessão precisa iniciar em uma data futura");
    }

    [TestMethod]
    public void Deve_Retornar_Erro_Se_Data_E_Hora_For_Passado_Hoje()
    {
        var horaPassada = DateTime.Now.TimeOfDay.Subtract(TimeSpan.FromMinutes(5));
        var sessao = new Sessão(
            filmeValido.Id,
            salaValida.Id,
            DateTime.Today,
            horaPassada)
        {
            Sala = salaValida,
            Filme = filmeValido
        };

        var erros = sessao.Validar();

        CollectionAssert.Contains(erros, "A sessão precisa iniciar em um horário futuro");
    }

    [TestMethod]
    public void Deve_Gerar_Assentos_Corretamente()
    {
        var sessao = new Sessão(
            filmeValido.Id,
            salaValida.Id,
            DateTime.Today.AddDays(1),
            new TimeSpan(20, 0, 0))
        {
            Sala = salaValida,
            Filme = filmeValido
        };

        var assentos = sessao.GerarAssentos();

        Assert.AreEqual(salaValida.Capacidade, assentos.Count);
        Assert.AreEqual("A - 1", assentos[0].Numero);
        Assert.AreSame(sessao, assentos[0].Sessao);
    }

    [TestMethod]
    public void Deve_Marcar_Sessao_Com_Finalizada_Manual()
    {
        var sessao = new Sessão(
            filmeValido.Id,
            salaValida.Id,
            DateTime.Today,
            DateTime.Now.AddMinutes(-filmeValido.Duracao).TimeOfDay)
        {
            Sala = salaValida,
            Filme = filmeValido
        };

        sessao.Encerrar();

        Assert.IsTrue(sessao.Finalizada);
    }

    [TestMethod]
    public void Deve_Marcar_Sessao_Com_Finalizada_Pelo_Tempo()
    {
        var sessao = new Sessão(
            filmeValido.Id,
            salaValida.Id,
            DateTime.Today,
            DateTime.Now.Subtract(TimeSpan.FromMinutes(filmeValido.Duracao + 10)).TimeOfDay)
        {
            Sala = salaValida,
            Filme = filmeValido
        };

        Assert.IsTrue(sessao.Finalizada);
    }
}
