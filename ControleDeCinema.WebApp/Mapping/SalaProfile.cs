using AutoMapper;
using ControleDeCinema.Domínio;
using ControleDeCinema.WebApp.Models;

namespace ControleDeCinema.WebApp.Mapping;

public class SalaProfile : Profile
{
    public SalaProfile()
    {
        CreateMap<Sala, ListarSalaViewModel>();
        CreateMap<Sala, EditarSalaViewModel>();
        CreateMap<Sala, ExcluirSalaViewModel>();
        CreateMap<Sala, DetalhesSalaViewModel>();

        CreateMap<EditarSalaViewModel, Sala>();
        CreateMap<CadastrarSalaViewModel, Sala>();

    }
}
