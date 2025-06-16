using AutoMapper;
using ControleDeCinema.Aplicação.AutenticaçãoService;
using System.Security.Authentication;

namespace ControleDeCinema.WebApp.Mapping.Resolvers;


public class UsuarioResolver : IValueResolver<object, object, int>
{
    private readonly AutenticacaoService _servicoAutenticacao;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsuarioResolver(AutenticacaoService servicoAutenticacao, IHttpContextAccessor httpContextAccessor)
    {
        _servicoAutenticacao = servicoAutenticacao;
        _httpContextAccessor = httpContextAccessor;
    }

    public int Resolve(object source, object destination, int destMember, ResolutionContext context)
    {
        var usuarioClaim = _httpContextAccessor.HttpContext?.User;

        var empresaId = _servicoAutenticacao.ObterIdEmpresaAsync(usuarioClaim!).Result;

        if (empresaId is null)
            throw new AuthenticationException("Não foi possível obter o ID da empresa requisitada!");

        return empresaId.Value;
    }
}
