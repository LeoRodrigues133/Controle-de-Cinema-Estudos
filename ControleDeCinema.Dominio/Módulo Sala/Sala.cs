

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

    public override List<string> Validar()
    {
        
        var erros = new List<string>();

        if (string.IsNullOrEmpty(Nome) || Nome.Length < 1)
            erros.Add("Nome inválido.");

        if (Capacidade < 1)
            erros.Add("Capacidade inválido.");

        return erros;
    }
}