namespace ControleDeCinema.Domínio.Compatilhado;
public interface IRepositorioBase<Generics> where Generics : EntidadeBase
{
    void Inserir(Generics entidade);
    void Editar(Generics entidade);
    void Excluir(Generics entidade);
    Generics SelecionarPorId(int id);
    List<Generics> SelecionarTodos();
    List<Generics> Filtrar(Func<Generics, bool> predicate);
}
