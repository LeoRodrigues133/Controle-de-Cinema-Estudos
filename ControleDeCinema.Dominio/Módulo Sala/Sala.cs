namespace ControleDeCinema.Domínio;

public class Sala : EntidadeBase
{
    public Sala()
    {
    }

    public Sala(int capacidade, string nome, bool disponivel)
    {
        Capacidade = capacidade;
        Nome = nome;
        Disponivel = disponivel;
    }

    public int Capacidade { get; set; }
    public string Nome { get; set; }
    public bool Disponivel { get; set; }


}