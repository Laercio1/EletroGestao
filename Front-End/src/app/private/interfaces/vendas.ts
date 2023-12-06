export interface IVendas {
    paginaAtual: number;
    totalPaginas: number;
    tamanhoPagina: number;
    totalItens: number;
    listaRetorno: IVendasListaRetorno[]
  }
  
  export interface IVendasListaRetorno {
    id: string,
    dataCadastro: string,
    idCliente: string,
    nomeCliente: string,
    idProduto: string,  
    nomeProduto: string,
    cep: string,
    regiao: string,
    numeroPedido: string,
    data: string,
    valorProduto: number,
    valorFrete: number,
    valorFinal: number,
    dataEntrega: string
  }
  
  