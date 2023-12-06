using EletroGestao.Dominio.PedidoRoot;

namespace EletroGestao.Application.Interfaces.Pedidos
{
    public interface IPedidoService : IDisposable
    {
        Task<bool> Registrar(Pedido pedido);
    }
}
