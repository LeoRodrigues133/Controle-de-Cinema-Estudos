namespace ControleDeCinema.Domínio;

public class Categoria : EntidadeBase
{
    public Categoria()
    {
    }
    public Categoria(string nome, int? filmeId)
    {
        Nome = nome;
        FilmeId = filmeId;

        Filmes = new List<Filme>();
    }   

    public string Nome { get; set; }
    public int? FilmeId { get; set; }
    public List<Filme> Filmes { get; set; }

}