using AutoMapper;
using EletroGestao.API.V1.Base;
using EletroGestao.Application.Interfaces.Clientes;
using EletroGestao.Application.Interfaces.Pedidos;
using EletroGestao.Application.Notificacoes;
using EletroGestao.Application.ViewModels.Pedido;
using EletroGestao.Dominio;
using EletroGestao.Dominio.ClienteRoot;
using EletroGestao.Dominio.ClienteRoot.Repository;
using EletroGestao.Dominio.Core.Utils.StringUtils;
using EletroGestao.Dominio.PedidoRoot;
using EletroGestao.Dominio.PedidoRoot.Repository;
using EletroGestao.Dominio.ProdutoRoot.Repository;
using EletroGestao.Infra.Comunicacao;
using EletroGestao.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace EletroGestao.API.V1.Controllers.Pedidos
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/pedido")]
    [ApiController]
    public class PedidoController : BaseController
    {
        private readonly EletroGestaoContext _dbContext;
        private readonly IPedidoService _pedidoService;
        private readonly IClienteService _clienteService;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public PedidoController(EletroGestaoContext dbContext,
                              INotificador notificador,
                              IPedidoService pedidoService,
                              IClienteService clienteService,
                              IPedidoRepository pedidoRepository,
                              IClienteRepository clienteRepository,
                              IProdutoRepository produtoRepository,
                              IMapper mapper) : base(notificador)
        {
            _dbContext = dbContext;
            _pedidoService = pedidoService;
            _clienteService = clienteService;
            _pedidoRepository = pedidoRepository;
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        protected override void Dispose(bool disposing)
        {
            _pedidoService?.Dispose();
            _clienteService?.Dispose();
            _pedidoRepository?.Dispose();
            _clienteRepository?.Dispose();
            _produtoRepository?.Dispose();
        }

        /// <summary>
        /// Upload planilha de Pedidos.
        /// </summary>
        /// <param name="viewmodel">View Model de planilha Pedidos.</param>
        /// <returns>Upload planilha de pedidos realizado.</returns>
        /// <remarks>
        /// Exemplo de requisição
        /// 
        ///     POST /api/v1/pedido/upload
        ///     {
        ///         "Planilha": "tipo de arquivo IFormFile"
        ///     }
        ///     
        ///  Campos obrigatórios: Planilha  
        ///  
        /// </remarks>
        /// <response code="200">Adicionada a planilha de pedidos com sucesso.</response>
        /// <response code="400">Não foi possível adicionar a planilha de pedidos.</response>
        /// 
        [HttpPost("upload")]
        [ProducesResponseType(typeof(RetornoSucesso), 200)]
        [ProducesResponseType(typeof(BadRequestRetorno), 400)]
        public async Task<IActionResult> Post([FromForm] PedidoAdicionarViewModel viewmodel)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var pedidosPlanilha = StringUtils.LerPlanilha(viewmodel.Planilha);

                    foreach (var cliente in pedidosPlanilha)
                    {
                        await ProcessarCliente(cliente);
                    }

                    foreach (var pedido in pedidosPlanilha)
                    {
                        await ProcessarPedido(pedido);
                    }

                    transaction.Commit();

                    return CustomResponse(new { Mensagem = "Upload planilha de pedidos concluído com sucesso." });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return CustomResponse("Erro ao processar upload planilha de pedidos.");
                }
            }
        }

        /// <summary>
        /// Retorna lista paginada de Pedidos.
        /// </summary>
        /// <returns></returns>
        /// <param name="id">Id (guid) do pedido.</param>
        /// <param name="idCliente">Id do cliente.</param>
        /// <param name="idProduto">Id do produto.</param>
        /// <param name="nomeCliente">Nome do cliente.</param>
        /// <param name="nomeProduto">Nome do produto.</param>
        /// <param name="cep">CEP.</param>
        /// <param name="numeroPedido">Número do pedido.</param>
        /// <param name="pagina">Página da lista de item.</param>
        /// <param name="tamanhoPagina">Total de itens por página.</param>
        /// <response code="200">O recurso solicitado foi processado e retornado com sucesso.</response>
        ///
        [HttpGet]
        [ProducesResponseType(typeof(SucessRetorno<ListaPaginada<PedidoViewModel>>), 200)]
        public async Task<IActionResult> GetPorTodosOsFiltros([FromQuery] Guid? id, 
        [FromQuery] Guid? idCliente,
        [FromQuery] Guid? idProduto,
        [FromQuery] string nomeCliente,
        [FromQuery] string nomeProduto,
        [FromQuery] string cep,
        [FromQuery] string numeroPedido,
        [FromQuery] int? pagina,
        [FromQuery] int? tamanhoPagina)
        {
            var models = await _pedidoRepository.ObterPorTodosFiltros(id, 
                idCliente, 
                idProduto,
                nomeCliente,
                nomeProduto,
                cep,
                numeroPedido,
                pagina,
                tamanhoPagina);

            ListaPaginada<PedidoViewModel> retorno = new ListaPaginada<PedidoViewModel>(
                _mapper.Map<List<PedidoViewModel>>(models.ListaRetorno),
                models.PaginaAtual,
                models.TotalPaginas,
                models.TamanhoPagina,
                models.TotalItens);

            return CustomResponse(retorno);
        }

        /// <summary>
        /// Retorna totais dos Pedidos.
        /// </summary>
        /// <returns>Resultado da operação.</returns>
        /// <remarks></remarks>
        /// <response code="200">Exibido com sucesso totais dos pedidos.</response>
        /// <response code="400">Não foi possível exibir totais dos pedidos.</response>
        /// 
        [HttpGet("totais-pedidos")]
        [ProducesResponseType(typeof(PedidoTotaisViewModel), 200)]
        public async Task<IActionResult> GetTotaisPedidos()
        {
            var models = await _pedidoRepository.BuscarTotaisPedidos();

            if (models == null)
            {
                NotificarErro("Pedido não encontrado na base de dados");
                return CustomResponse();
            }

            return CustomResponse(models);
        }

        private async Task ProcessarCliente(Planilha pedido)
        {
            var clienteExiste = _clienteRepository.VerificaClienteExiste(pedido.CPF_CNPJ);

            if (clienteExiste == null)
            {
                var novoCliente = new Cliente
                {
                    NomeRazaoSocial = pedido.NomeRazaoSocial,
                    Documento = StringUtils.ApenasNumeros(pedido.CPF_CNPJ)
                };

                await _clienteService.Registrar(novoCliente);
            }
        }

        private async Task ProcessarPedido(Planilha pedido)
        {
            var pedidoGerado = GerarNovoPedido(pedido);

            var novoPedido = new Pedido
            {
                IdCliente = pedidoGerado.IdCliente,
                NomeCliente = pedidoGerado.NomeCliente,
                IdProduto = pedidoGerado.IdProduto,
                NomeProduto = pedidoGerado.NomeProduto,
                CEP = pedido.CEP,
                Regiao = pedidoGerado.Regiao,
                NumeroPedido = pedido.NumeroPedido,
                Data = Convert.ToDateTime(pedido.Data),
                ValorProduto = pedidoGerado.ValorProduto,
                ValorFrete = pedidoGerado.ValorFrete,
                ValorFinal = pedidoGerado.ValorFinal,
                DataEntrega = pedidoGerado.DataEntrega
            };

            await _pedidoService.Registrar(novoPedido);
        }

        private Pedido GerarNovoPedido(Planilha pedido)
        {
            var (idCliente, nomeCliente) = ObterInformacoesCliente(pedido.CPF_CNPJ);
            var (idProduto, nomeProduto, valorProduto) = ObterInformacoesProduto(pedido.Produto);
            var (valorFrete, dataEntrega, regiao) = ObterInformacoesPedido(pedido.CEP, pedido.Data, valorProduto);

            return new Pedido
            {
                IdCliente = idCliente,
                NomeCliente = nomeCliente,
                IdProduto = idProduto,
                NomeProduto = nomeProduto,
                ValorProduto = valorProduto,
                ValorFrete = valorFrete,
                DataEntrega = dataEntrega,
                ValorFinal = valorProduto + valorFrete,
                Regiao = regiao
            };
        }

        private (Guid idCliente, string nomeCliente) ObterInformacoesCliente(string cpfCnpj)
        {
            var cliente = _clienteRepository.ObterCliente(cpfCnpj);
            return (cliente.Id, cliente.NomeRazaoSocial);
        }

        private (Guid idProduto, string nomeProduto, decimal valorProduto) ObterInformacoesProduto(string nomeProduto)
        {
            var produto = _produtoRepository.ObterProduto(nomeProduto);
            return (produto.Id, produto.NomeProduto, produto.Valor);
        }

        private (decimal valorFrete, DateTime dataEntrega, string regiao) ObterInformacoesPedido(string cep, string data, decimal valorProduto)
        {
            var localizacao = EletroGestaoComunicacao.ObterInformacoesLocalizacao(StringUtils.ApenasNumeros(cep));
            var valores = CalcularValoresPedido.ObterValoresPedido(localizacao, Convert.ToDateTime(data), valorProduto);
            return (valores.ValorFrete, valores.DataEntrega, valores.Regiao);
        }
    }
}
