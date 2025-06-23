using ControleDeCinema.Domínio;
using ControleDeCinema.Infra.Compartilhado;
using ControleDeCinema.Infra.Módulo_Assento;
using ControleDeCinema.Infra.Módulo_Categoria;
using ControleDeCinema.Infra.Módulo_Filme;
using ControleDeCinema.Infra.Módulo_Sala;
using ControleDeCinema.Infra.Módulo_Sessão;
using FizzWare.NBuilder;

namespace ControleDeCinema.Integracao.Compartilhado;
public abstract class RepositorioEmOrmBaseTest
{
    protected CinemaDbContext _dbContext;
    protected RepositorioSalaEmOrm _repositorioSala;
    protected RepositorioFilmeEmOrm _repositorioFilme;
    protected RepositorioSessaoEmOrm _repositorioSessao;
    protected RepositorioAssentoEmOrm _repositorioAssento;
    protected RepositorioCategoriaEmOrm _repositorioCategoria;

    protected Usuario usuarioAutenticado;

    [TestInitialize]
    public void Inicializar()
    {
        _dbContext = new CinemaDbContext();

        _dbContext.Salas.RemoveRange(_dbContext.Salas);
        _dbContext.Filmes.RemoveRange(_dbContext.Filmes);
        _dbContext.Sessaos.RemoveRange(_dbContext.Sessaos);
        _dbContext.Assentos.RemoveRange(_dbContext.Assentos);
        _dbContext.Categorias.RemoveRange(_dbContext.Categorias);

        _dbContext.Usuarios.RemoveRange(_dbContext.Usuarios);

        usuarioAutenticado = new Usuario()
        {
            UserName = "test",
            Email = "test@email.com",
        };

        _dbContext.Usuarios.Add(usuarioAutenticado);

        _dbContext.SaveChanges();

        _repositorioSala = new RepositorioSalaEmOrm(_dbContext);
        _repositorioFilme = new RepositorioFilmeEmOrm(_dbContext);
        _repositorioSessao = new RepositorioSessaoEmOrm(_dbContext);
        _repositorioAssento = new RepositorioAssentoEmOrm(_dbContext);
        _repositorioCategoria = new RepositorioCategoriaEmOrm(_dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Sala>(_repositorioSala.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<Filme>(_repositorioFilme.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<Sessão>(_repositorioSessao.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<Assento>(_repositorioAssento.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<Categoria>(_repositorioCategoria.Inserir);
    }
}
