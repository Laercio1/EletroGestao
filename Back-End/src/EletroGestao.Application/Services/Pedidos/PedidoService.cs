using EletroGestao.Application.Interfaces.Pedidos;
using EletroGestao.Application.Notificacoes;
using EletroGestao.Application.Services.Base;
using EletroGestao.Dominio.PedidoRoot.Repository;
using EletroGestao.Dominio.PedidoRoot;
using EletroGestao.Dominio.PedidoRoot.Validation;

namespace EletroGestao.Application.Services.Pedidos
{
    public class PedidoService : BaseService, IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(INotificador notificador,
                                IPedidoRepository repository) : base(notificador)
        {
            _pedidoRepository = repository;
        }

        public void Dispose()
        {
            _pedidoRepository.Dispose();
        }

        public async Task<bool> Registrar(Pedido pedido)
        {
            ExecutarValidacao(new PedidoValidation(), pedido);

            if (_notificador.TemNotificacao())
                return false;

            try
            {
                await _pedidoRepository.Adicionar(pedido);
            }
            catch (Exception ex)
            {
                Notificar(string.Format("Não foi possível adicionar Pedido. Motivo: {0}", ex.Message));
            }

            return !_notificador.TemNotificacao();
        }
    }
}
