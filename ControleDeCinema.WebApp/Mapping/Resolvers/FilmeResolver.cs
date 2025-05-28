using AutoMapper;
using ControleDeCinema.Domínio;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeCinema.WebApp.Mapping.Resolvers;

public class FilmeResolver : IValueResolver<object, object, IEnumerable<SelectListItem>?>
{

    readonly IRepositorioFilme _repositorioFilme;

    public FilmeResolver(IRepositorioFilme repositorioFilme)
    {
        _repositorioFilme = repositorioFilme;
    }

    public IEnumerable<SelectListItem>? Resolve(object source, object destination, IEnumerable<SelectListItem>? destMember, ResolutionContext context)
    {
        return _repositorioFilme
            .SelecionarTodos()
            .Select(x => new SelectListItem(
                    x.Id.ToString(),
                    x.Nome
                )
            );
    }
}
