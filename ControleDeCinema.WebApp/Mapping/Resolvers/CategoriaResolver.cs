using AutoMapper;
using ControleDeCinema.Domínio;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeCinema.WebApp.Mapping.Resolvers;

public class CategoriaResolver : IValueResolver<object, object, IEnumerable<SelectListItem>?>
{
    readonly IRepositorioCategoria _repositorioCategoria;

    public CategoriaResolver(IRepositorioCategoria repositorioCategoria)
    {
        _repositorioCategoria = repositorioCategoria;
    }

    public IEnumerable<SelectListItem>? Resolve(object? source, object destination, IEnumerable<SelectListItem>? destMember, ResolutionContext context)
    {
        return _repositorioCategoria
            .SelecionarTodos()
            .Select(x => new SelectListItem
                (
                    x.Id.ToString(),
                    x.Nome
                )
            );
    }
}
