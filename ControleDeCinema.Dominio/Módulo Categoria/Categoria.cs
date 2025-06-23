
namespace ControleDeCinema.Domínio;

public class Categoria : EntidadeBase
{
    public Categoria()
    {
    }
    public Categoria(string nome)
    {
        Nome = nome;

        //Filmes = new List<Filme>();
    }

    public string Nome { get; set; }

    public override List<string> Validar()
    {
        var erros = new List<string>();

        if (string.IsNullOrEmpty(Nome) || Nome.Length < 1)
            erros.Add("Nome inválido.");

        return erros;
    }

    //public List<Filme> Filmes { get; set; }
}