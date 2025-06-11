using ControleDeCinema.Domínio;
using ControleDeCinema.Domínio.Compatilhado;
using ControleDeCinema.WebApp.Models;
using FluentResults;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeCinema.WebApp.Extensions;

public static class DataPreviewExtensions
{

    public static FilmeFormViewModel? CarregarDadosAbstratos<T>(
        IRepositorioBase<T> repositorio,
        Func<T, string> texto,
        Func<T, string> valor,
        object? Mensagem) where T : EntidadeBase
    {
        var listResult = repositorio.SelecionarTodos();

        foreach (var r in listResult)
            if (r is Result)
            {

                Result<T> falha = r.ToResult();

                if (falha.IsFailed)
                {

                    Mensagem = new MensagemViewModel
                    {
                        Titulo = "Falha",
                        Mensagem = falha.Errors[0].Message,
                    };

                    return null;
                }


                var dados = falha.Value;

            }
        var viewModel = new FilmeFormViewModel
        {
            Categorias = null
        };

        return viewModel;
    }
}
