using EletroGestao.Application.ViewModels.Base;

namespace EletroGestao.Application.ViewModels.Pedido
{
    public class PedidoViewModel : BaseViewModelCadastro
    {
        public DateTime DataCadastro { get; set; }
        public string Id { get; set; }
        public Guid IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public Guid IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public string CEP { get; set; }
        public string Regiao { get; set; }
        public string NumeroPedido { get; set; }
        public DateTime Data { get; set; }
        public decimal ValorProduto { get; set; }
        public decimal ValorFrete { get; set; }
        public decimal ValorFinal { get; set; }
        public DateTime DataEntrega { get; set; }
    }
}
