using AutoMapper;
using ControleDeCinema.Domínio;
using ControleDeCinema.WebApp.Models;

namespace ControleDeCinema.WebApp.Mapping;

public class SessaoProfile : Profile
{
    public SessaoProfile()
    {
        CreateMap<Sessão, ListarSessaoViewModel>()
            .ForMember(x => x.Filme, dest => dest.MapFrom(src => src.Filme))
            .ForMember(x => x.Sala, dest => dest.MapFrom(src => src.Sala))
            .ForMember(x => x.HorarioDaSessao, dest => dest.MapFrom(src => src.HorarioDaSessao));

        CreateMap<Sessão, DetalhesSessaoViewModel>()
            .ForMember(x => x.Filme, dest => dest.MapFrom(src => src.Filme))
            .ForMember(x => x.Sala, dest => dest.MapFrom(src => src.Sala))
            .ForMember(x => x.DataDeExibicao, dest => dest.MapFrom(src => src.DataDeExibicao))
            .ForMember(x => x.HorarioDaSessao, dest => dest.MapFrom(src => src.HorarioDaSessao));

        CreateMap<Sessão, EncerrarSessaoViewModel>()
            .ForMember(x => x.Filme, dest => dest.MapFrom(src => src.Filme))
            .ForMember(x => x.Sala, dest => dest.MapFrom(src => src.Sala))
            .ForMember(x => x.DataDeExibicao, dest => dest.MapFrom(src => src.DataDeExibicao))
            .ForMember(x => x.HorarioDaSessao, dest => dest.MapFrom(src => src.HorarioDaSessao));

        CreateMap<Sessão, SessaoFormViewModel>();
        CreateMap<SessaoFormViewModel, Sessão>();


        CreateMap<Assento, AssentoViewModel>();
        CreateMap<Sessão, VenderSessaoViewModel>();

    }
}
