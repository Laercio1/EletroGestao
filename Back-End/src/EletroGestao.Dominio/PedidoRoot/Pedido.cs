using EletroGestao.Dominio.Core.Annotations;
using EletroGestao.Dominio.Core.Models;

namespace EletroGestao.Dominio.PedidoRoot
{
    [Table("Pedidos")]
    public class Pedido : Entity
    {
        public DateTime DataCadastro { get; set; }

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
