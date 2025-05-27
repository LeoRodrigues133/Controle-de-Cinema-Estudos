using AutoMapper;
using ControleDeCinema.Domínio;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeCinema.WebApp.Mapping.Resolvers;

public class SalaResolver : IValueResolver<object, object, IEnumerable<SelectListItem>?>
{
    readonly IRepositorioSala _repositorioSala;

    public SalaResolver(IRepositorioSala repositorioSala)
    {
        _repositorioSala = repositorioSala;
    }

    public IEnumerable<SelectListItem>? Resolve
        (object source,
        object destination,
        IEnumerable<SelectListItem>? destMember,
        ResolutionContext context)
    {
        return _repositorioSala
            .SelecionarTodos()
            .Select(x => new SelectListItem
                (
                    x.Id.ToString(),
                    x.Nome
                )
            );
    }
}
