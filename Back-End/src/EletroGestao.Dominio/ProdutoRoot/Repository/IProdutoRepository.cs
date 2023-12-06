using EletroGestao.Dominio.Interfaces;

namespace EletroGestao.Dominio.ProdutoRoot.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Produto ObterProduto(string nomeProduto);
    }
}
