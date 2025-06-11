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

}