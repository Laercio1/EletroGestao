using AutoMapper;
using EletroGestao.Application.Notificacoes;
using EletroGestao.Application.Services.Base.Interfaces;
using EletroGestao.Application.ViewModels.Base;
using EletroGestao.Dominio.Core.Models;
using EletroGestao.Dominio.Interfaces;
using FluentValidation;

namespace EletroGestao.Application.Services.Base
{
    public abstract class BaseCadastroService<TModel, TViewModel, TViewModelAdicionar, TViewModelAtualizar, TValidator>
       : BaseService,
       IBaseCadastroService<TModel, TViewModel, TViewModelAdicionar, TViewModelAtualizar, TValidator>
       where TModel : Entity, new()
       where TValidator : AbstractValidator<TModel>, new()
       where TViewModelAtualizar : BaseViewModelCadastro, new()
    {
        protected readonly IRepository<TModel> _repository;
        protected readonly IMapper _mapper;

        protected string _NomeDominio { get; private set; }

        public BaseCadastroService(string NomeDominio,
                            INotificador notificador,
                            IRepository<TModel> repository,
                            IMapper mapper)
            : base(notificador)
        {
            _repository = repository;
            _NomeDominio = NomeDominio;
            _mapper = mapper;
        }

        /// <summary>
        /// Valida model com base na nas especificações feitas com FlentValidation.
        /// </summary>
        /// <param name="model">Model a ser validado.</param>
        /// <returns>True model passou por todas validações, False model inválido.</returns>
        public virtual bool ValidarModel(TModel model)
        {
            if (!ExecutarValidacao(new TValidator(), model))
                return false;
            return true;
        }

        /// <summary>
        /// Mapea domínio com base em um ViewModel utilizando AutoMapper.
        /// Método aceita ovverride para especificar ações.
        /// </summary>
        /// <param name="viewmodel">ViewModel representando o Model.</param>
        /// <returns></returns>
        public virtual TModel MapearDominio(TViewModelAdicionar viewmodel)
        {
            TModel model = _mapper.Map<TModel>(viewmodel);
            return model;
        }

        /// <summary>
        /// Validação se há relacinamento com chave estrangeira do Model com outro que dependa dele.
        /// Utilizado para verificar as relações no momento da exclusão.
        /// </summary>
        /// <param name="id">Id do Model a ser verificado.</param>
        /// <returns>True não existe relacinamento estrageiro, Falso model tem relacionamento.</returns>
        public virtual bool ValidarModelSemRelacionamentos(Guid id)
        {
            return true;
        }

        /// <summary>
        /// Valida ações para adicionar um Model, utilizado para verificar caso já exista um domínio
        /// com mesma descrição ou outro campo com mesmo valor.
        /// 
        /// Caso tenha especificidade o método deve ser substituido. 
        /// </summary>
        /// <param name="model">Model a ser validado para adicionar.</param>
        /// <returns>True o model é valido para acidionar, Falso model inválido.</returns>
        public virtual bool ValidarAdicionarModel(TModel model)
        {
            return true;
        }

        /// <summary>
        /// Mapeia atualizações no Model com base em um ViewModel utilizando métodos em reflection.
        /// Somente é alterado campos com valores diferentes entre o Model e o ViewModel, marcando a flag
        /// </summary>
        /// <param name="model">Model a ser atualizado.</param>
        /// <param name="viewmodel">ViewModel com os campos e valores para a atualização.</param>
        /// <returns>True se houve alguma alteraççao no Model, False não ocorreu nenhuma atualização.</returns>
        public virtual bool MapearAtualizacoes(TModel model, TViewModelAtualizar viewmodel)
        {
            return _repository.UpdateValeuWithViewModel(model, viewmodel);
        }

        /// <summary>
        /// Valida se é possível ser excluído um model. Utilizado para ser adicionado condições que não deixe
        /// o model ser excluído com base em regras de negocio, por exemplo: se Status = Processado não é possível excluir.        
        /// </summary>
        /// <param name="model">Model a ser validado.</param>
        /// <returns>True o model é valido para exclusão, Falso model não pode ser excluído.</returns>
        public virtual bool ValidarExclusao(TModel model)
        {
            return true;
        }

        public virtual async Task<bool> Adicionar(TModel model)
        {
            if (!ValidarModel(model))
                return false;

            if (!ValidarAdicionarModel(model))
                return false;

            try
            {
                await _repository.Adicionar(model);
            }
            catch (Exception ex)
            {
                Notificar("Não é possível adicionar {0}. Motivo: {1}.", _NomeDominio, ex.InnerException.Message);
                return false;
            }

            return true;
        }

        public virtual async Task<bool> Atualizar(TViewModelAtualizar viewmodel)
        {
            if (!_repository.DominioExiste(viewmodel.Id))
            {
                Notificar("{0} não existe.", _NomeDominio);
                return false;
            }

            TModel model = await _repository.ObterPorId(viewmodel.Id);

            bool temAtualizacao = MapearAtualizacoes(model, viewmodel);

            if (!temAtualizacao)
            {
                Notificar("Não há alterações no registro de {0}.", _NomeDominio);
                return false;
            }

            if (!ValidarModel(model))
                return false;

            try
            {
                await _repository.Atualizar(model);
                return true;
            }
            catch (Exception ex)
            {
                Notificar("Não foi possível atualizar o registro de {0}. Motivo: {1}", _NomeDominio, ex.InnerException);
                return false;
            }
        }

        public virtual async Task<bool> Remover(Guid id)
        {
            if (!_repository.DominioExiste(id))
            {
                Notificar("{0} não existe.", _NomeDominio);
                return false;
            }

            TModel model = await _repository.ObterPorId(id);

            if (!ValidarExclusao(model))
            {
                return false;
            }

            if (!ValidarModelSemRelacionamentos(id))
            {
                Notificar("Não é possível excluir {0}.", _NomeDominio);
                return false;
            }

            try
            {
                await _repository.Remover(id);
                return true;
            }
            catch (Exception ex)
            {
                Notificar("Não foi possível excluir o {0}. Motivo: {1}", _NomeDominio, ex.InnerException.Message);
                return false;
            }
        }
    }
}
