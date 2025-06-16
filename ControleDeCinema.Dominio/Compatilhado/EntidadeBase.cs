namespace ControleDeCinema.Domínio;
public abstract class EntidadeBase
{
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public Usuario? Empresa { get; set; }
}
