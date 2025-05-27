namespace ControleDeCinema.WebApp.Models;

public class ListarSalaViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Capacidade { get; set; }
    //public bool Disponivel { get; set; }
}

public class CadastrarSalaViewModel
{
    public string Nome { get; set; }
    public int Capacidade { get; set; }

}
public class ExcluirSalaViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Capacidade { get; set; }
}
public class EditarSalaViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Capacidade { get; set; }
}
public class DetalhesSalaViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Capacidade { get; set; }
}