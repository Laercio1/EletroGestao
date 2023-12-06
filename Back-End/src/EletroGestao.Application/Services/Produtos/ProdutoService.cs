using EletroGestao.Application.Notificacoes;
using EletroGestao.Application.Services.Base;
using EletroGestao.Application.Interfaces.Produtos;
using EletroGestao.Dominio.ProdutoRoot;
using EletroGestao.Dominio.ProdutoRoot.Validation;
using EletroGestao.Dominio.ProdutoRoot.Repository;

namespace EletroGestao.Application.Services.Produtos
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(INotificador notificador,
                                IProdutoRepository repository) : base(notificador)
        {
            _produtoRepository = repository;
        }

        public void Dispose()
        {
            _produtoRepository.Dispose();
        }

        public async Task<bool> Registrar(Produto produto)
        {
            ExecutarValidacao(new ProdutoValidation(), produto);

            if (_notificador.TemNotificacao())
                return false;

            try
            {
                await _produtoRepository.Adicionar(produto);
            }
            catch (Exception ex)
            {
                Notificar(string.Format("Não foi possível adicionar Produto. Motivo: {0}", ex.Message));
            }

            return !_notificador.TemNotificacao();
        }
    }
}
