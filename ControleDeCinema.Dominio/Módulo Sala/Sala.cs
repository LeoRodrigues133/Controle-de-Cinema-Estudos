
namespace ControleDeCinema.Domínio;

public class Sala : EntidadeBase
{
    public Sala()
    {
    }

    public Sala(int capacidade, string nome)
    {
        Capacidade = capacidade;
        Nome = nome;
        Disponivel = true;

    }

    public int Capacidade { get; set; }
    public string Nome { get; set; }
    public bool Disponivel { get; set; }

    public void Ocupar()
    {
        Disponivel = false;
    }

}