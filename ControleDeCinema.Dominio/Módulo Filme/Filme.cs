namespace ControleDeCinema.Domínio;

public class Filme : EntidadeBase
{
    public Filme()
    {
    }

    public Filme(string nome, int duracao, int categoriaId)
    {
        Nome = nome;
        Duracao = duracao;
        CategoriaId = categoriaId;

        //Categorias = new List<Categoria>();
    }

    //public List<Categoria> Categorias { get; set; }
    public string Nome { get; set; }
    public int Duracao { get; set; }
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
}