using AutoMapper;
using ControleDeCinema.Domínio;
using ControleDeCinema.WebApp.Mapping.Resolvers;
using ControleDeCinema.WebApp.Models;

namespace ControleDeCinema.WebApp.Mapping;

public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        CreateMap<Filme, ListarFilmeViewModel>()
            .ForMember(x => x.Categoria, dest => dest.MapFrom(src => src.Categoria));

        CreateMap<Filme, EditarFilmeViewModel>();

        CreateMap<Filme, DetalhesFilmeViewModel>()
            .ForMember(x => x.Categoria, dest => dest.MapFrom(src => src.Categoria));

        CreateMap<Filme, ExcluirFilmeViewModel>()
            .ForMember(x => x.Categoria, dest => dest.MapFrom(src => src.Categoria));

        CreateMap<FilmeFormViewModel, Filme>()
            .ForMember(x => x.EmpresaId, dest => dest.MapFrom<UsuarioResolver>());

        CreateMap<EditarFilmeViewModel, Filme>()
            .ForMember(x => x.Categoria, dest => dest.MapFrom(src => src.Categoria));
        

    }
}
