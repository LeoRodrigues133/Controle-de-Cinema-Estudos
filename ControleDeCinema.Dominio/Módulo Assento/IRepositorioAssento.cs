namespace ControleDeCinema.Domínio.Módulo_Assento;
public interface IRepositorioAssento
{
    void Inserir(Assento assento);
    void Excluir (Assento assento);
    Assento SelecionarPorId (int id);
}
