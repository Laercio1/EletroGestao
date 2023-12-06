using EletroGestao.Infra.Data.Context;
using EletroGestao.Infra.Data.Repository.Base;
using EletroGestao.Dominio.ProdutoRoot;
using EletroGestao.Dominio.ProdutoRoot.Repository;

namespace EletroGestao.Infra.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(EletroGestaoContext context) : base(context)
        {

        }

        public Produto ObterProduto(string nomeProduto)
        {
            var produto = Db.Produto.Where(p => p.NomeProduto.ToLower() == nomeProduto.ToLower()).FirstOrDefault();

            return produto;
        }
    }
}
