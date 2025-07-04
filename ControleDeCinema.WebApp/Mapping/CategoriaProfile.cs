﻿using AutoMapper;
using ControleDeCinema.Domínio;
using ControleDeCinema.WebApp.Mapping.Resolvers;
using ControleDeCinema.WebApp.Models;

namespace ControleDeCinema.WebApp.Mapping;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<Categoria, ListarCategoriaViewModel>();
        CreateMap<Categoria, EditarCategoriaViewModel>();
        CreateMap<Categoria, ExcluirCategoriaViewModel>();
        CreateMap<Categoria, DetalhesCategoriaViewModel>();

        CreateMap<EditarCategoriaViewModel, Categoria>();
        CreateMap<CadastrarCategoriaViewModel, Categoria>()
            .ForMember(x => x.EmpresaId, dest => dest.MapFrom<UsuarioResolver>());

    }
}
