namespace ControleDeCinema.Domínio;

public class Sessão : EntidadeBase
{
    public Sessão()
    {
    }

    public Sessão(int filmeId, int salaId, DateTime dataDeExibicao, TimeSpan horaDeExibicao)
    {
        FilmeId = filmeId;
        SalaId = salaId;
        DataDeExibicao = dataDeExibicao;
        HorarioDaSessao = horaDeExibicao;

        Assentos = new List<Assento>();
        HorarioDaSessao = horaDeExibicao;
    }

    private bool _finalizada;
    public bool Finalizada {
        get
        {
            return _finalizada || HorarioDaSessao.Add(TimeSpan.FromMinutes(Convert.ToDouble(Filme.Duracao))) < DateTime.Now.TimeOfDay;


        }
        set => _finalizada = value;
        }

    public int FilmeId { get; set; }
    public int SalaId { get; set; }
    public Sala? Sala { get; set; }
    public Filme? Filme { get; set; }
    public DateTime DataDeExibicao { get; set; }
    public TimeSpan HorarioDaSessao { get; set; }
    public List<Assento> Assentos { get; set; }

    public void Encerrar()
    {
        Finalizada = true;
    }
    private string GerarNumeroAssento(int Numero)
    {
        char Fileira = (char)('A' + (Numero - 1) / 10);


        string Identificacao = $"{Fileira} - {Numero}";

        return Identificacao;
    }

    public List<Assento> GerarAssentos()
    {
        for (int i = 1; i < Sala!.Capacidade + 1; i++)
        {

            Assento novoAssento = new Assento
            {
                Numero = GerarNumeroAssento(i),
                EmpresaId = this.EmpresaId,
                Sessao = this
            };
            this.Assentos.Add(novoAssento);
        }

        return Assentos;
    }

}