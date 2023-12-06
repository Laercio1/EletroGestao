using EletroGestao.Dominio;
using EletroGestao.Infra.Data.Repository.Base;
using EletroGestao.Dominio.PedidoRoot;
using EletroGestao.Dominio.PedidoRoot.Repository;
using EletroGestao.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EletroGestao.Infra.Data.Repository
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(EletroGestaoContext context) : base(context)
        {

        }

        public async Task<ListaPaginada<Pedido>> ObterPorTodosFiltros(Guid? id,
        Guid? idCliente,
        Guid? idProduto,
        string nomeCliente,
        string nomeProduto,
        string cep,
        string numeroPedido,
        int? pagina,
        int? tamanhoPagina)
        {
            IQueryable<Pedido> _consulta = Db.Pedido;

            if (id.HasValue)
                _consulta = _consulta.Where(e => e.Id == id);

            if (idCliente.HasValue)
                _consulta = _consulta.Where(e => e.IdCliente == idCliente);

            if (idProduto.HasValue)
                _consulta = _consulta.Where(e => e.IdProduto == idProduto);

            if (!string.IsNullOrEmpty(nomeCliente))
                _consulta = _consulta.Where(c => EF.Functions.Like(c.NomeCliente, nomeCliente.ToScape()));

            if (!string.IsNullOrEmpty(nomeProduto))
                _consulta = _consulta.Where(c => EF.Functions.Like(c.NomeProduto, nomeProduto.ToScape()));

            if (!string.IsNullOrEmpty(cep))
                _consulta = _consulta.Where(c => EF.Functions.Like(c.CEP, cep.ToScape()) ||
                EF.Functions.Like(c.CEP.Replace(".", "").Replace("-", "").Replace("/", ""), cep.ToScape()));

            if (!string.IsNullOrEmpty(numeroPedido))
                _consulta = _consulta.Where(c => EF.Functions.Like(c.NumeroPedido, numeroPedido.ToScape()));

            _consulta = _consulta.OrderBy(e => e.DataEntrega);

            _paginated = await ReturnPaginatedList(_consulta, pagina, tamanhoPagina);

            return new ListaPaginada<Pedido>(_paginated, _paginated.PageIndex, _paginated.TotalPages, _paginated.PageSize, _paginated.TotalItens);
        }

        public async Task<PedidoTotais> BuscarTotaisPedidos()
        {
            IQueryable<Pedido> _consulta = Db.Pedido;

            PedidoTotais pedidoTotal = new PedidoTotais();

            var regioes = new List<string> { "Norte/Nordeste", "Centro-oeste/Sul", "Sudeste", "São Paulo Capital" };
            var produtos = new List<string> { "Celular", "Notebook", "Televisão" };

            await CalcularTotaisPorRegiao(_consulta, pedidoTotal, regioes);
            await CalcularTotaisPorProduto(_consulta, pedidoTotal, produtos);

            return pedidoTotal;
        }

        private async Task CalcularTotaisPorRegiao(IQueryable<Pedido> consulta, PedidoTotais pedidoTotal, List<string> regioes)
        {
            foreach (var regiao in regioes)
            {
                var totalPedidos = await consulta.Where(p => p.Regiao == regiao).CountAsync();
                var pedidosRegiao = await consulta.Where(p => p.Regiao == regiao).ToListAsync();
                var totalValor = pedidosRegiao.Sum(p => p.ValorFinal);

                switch (regiao)
                {
                    case "Norte/Nordeste":
                        pedidoTotal.PedidosTotalRegiaoNorteNordeste = totalPedidos;
                        pedidoTotal.PedidosValorTotalRegiaoNorteNordeste = totalValor;
                        break;
                    case "Centro-oeste/Sul":
                        pedidoTotal.PedidosTotalRegiaoCentroOesteSul = totalPedidos;
                        pedidoTotal.PedidosValorTotalRegiaoCentroOesteSul = totalValor;
                        break;
                    case "Sudeste":
                        pedidoTotal.PedidosTotalRegiaoSudeste = totalPedidos;
                        pedidoTotal.PedidosValorTotalRegiaoSudeste = totalValor;
                        break;
                    case "São Paulo Capital":
                        pedidoTotal.PedidosTotalRegiaoSaoPauloCapital = totalPedidos;
                        pedidoTotal.PedidosValorTotalRegiaoSaoPauloCapital = totalValor;
                        break;
                }
            }
        }

        private async Task CalcularTotaisPorProduto(IQueryable<Pedido> consulta, PedidoTotais pedidoTotal, List<string> produtos)
        {
            foreach (var produto in produtos)
            {
                var totalPedidos = await consulta.Where(p => p.NomeProduto == produto).CountAsync();
                var pedidosRegiao = await consulta.Where(p => p.NomeProduto == produto).ToListAsync();
                var totalValor = pedidosRegiao.Sum(p => p.ValorFinal);

                switch (produto)
                {
                    case "Celular":
                        pedidoTotal.PedidosTotalCelular = totalPedidos;
                        pedidoTotal.PedidosValorTotalCelular = totalValor;
                        break;
                    case "Notebook":
                        pedidoTotal.PedidosTotalNotebook = totalPedidos;
                        pedidoTotal.PedidosValorTotalNotebook = totalValor;
                        break;
                    case "Televisão":
                        pedidoTotal.PedidosTotalTelevisao = totalPedidos;
                        pedidoTotal.PedidosValorTotalTelevisao = totalValor;
                        break;
                }
            }
        }
    }
}
