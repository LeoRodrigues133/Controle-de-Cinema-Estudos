using AutoMapper;
using ControleDeCinema.Domínio;
using ControleDeCinema.WebApp.Mapping.Resolvers;
using ControleDeCinema.WebApp.Models;

namespace ControleDeCinema.WebApp.Mapping;

public class AssentoProfile : Profile
{
    public AssentoProfile()
    {
        CreateMap<Assento, AssentoViewModel>();
        CreateMap<AssentoViewModel, Assento>()
            .ForMember(x => x.EmpresaId, dest => dest.MapFrom<UsuarioResolver>());
    }

}
