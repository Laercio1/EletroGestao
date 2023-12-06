using EletroGestao.Application.ViewModels.Base;
using FluentValidation;

namespace EletroGestao.Application.Services.Base.Interfaces
{
    public interface IBaseCadastroService<TModel, TViewModel, TViewModelAdicionar, TViewModelAtualizar, TValidator>
        where TValidator : AbstractValidator<TModel>, new()
        where TViewModelAtualizar : BaseViewModelCadastro, new()
    {
        bool ValidarModel(TModel model);
        bool ValidarModelSemRelacionamentos(Guid id);
        TModel MapearDominio(TViewModelAdicionar viewmodel);
        Task<bool> Adicionar(TModel model);
        Task<bool> Atualizar(TViewModelAtualizar viewmodel);
        Task<bool> Remover(Guid id);
    }
}
