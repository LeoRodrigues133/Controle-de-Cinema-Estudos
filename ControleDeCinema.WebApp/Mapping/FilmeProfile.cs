using AutoMapper;
using ControleDeCinema.Domínio;
using ControleDeCinema.WebApp.Models;

namespace ControleDeCinema.WebApp.Mapping;

public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        CreateMap<Filme, ListarFilmeViewModel>();
        CreateMap<Filme, EditarFilmeViewModel>();
        CreateMap<Filme, DetalhesFilmeViewModel>();
        CreateMap<Filme, ExcluirFilmeViewModel>();

        CreateMap<CadastrarFilmeViewModel, Filme>();
        CreateMap<EditarFilmeViewModel, Filme>();

    }
}
