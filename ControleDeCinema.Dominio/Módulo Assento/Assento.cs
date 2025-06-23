
namespace ControleDeCinema.Domínio;

public class Assento : EntidadeBase
{
    public Assento()
    {
    }
    public Assento(string numero, int sessaoId)
    {
        Numero = numero;
        SessaoId = sessaoId;

        Disponivel = false;
    }

    public string Numero { get; set; }
    public Sessão? Sessao{ get; set; }
    public bool Disponivel { get; set; }
    public int SessaoId { get; set; }


    public void OcuparAssento()
    {
        Disponivel = true;
    }

    public override List<string> Validar()
    {
        var erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Numero))
            erros.Add("O número do assento é obrigatório.");

        if (SessaoId <= 0)
            erros.Add("Sessão inválida para o assento.");

        return erros;
    }
}