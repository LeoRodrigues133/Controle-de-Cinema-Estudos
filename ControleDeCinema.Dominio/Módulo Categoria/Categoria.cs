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
    //public List<Filme> Filmes { get; set; }
}