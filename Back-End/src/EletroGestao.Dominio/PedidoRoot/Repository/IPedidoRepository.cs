using EletroGestao.Dominio.Interfaces;

namespace EletroGestao.Dominio.PedidoRoot.Repository
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<PedidoTotais> BuscarTotaisPedidos();

        Task<ListaPaginada<Pedido>> ObterPorTodosFiltros(Guid? id,
        Guid? idCliente,
        Guid? idProduto,
        string nomeCliente,
        string nomeProduto,
        string cep,
        string numeroPedido,
        int? pagina,
        int? tamanhoPagina);
    }
}
