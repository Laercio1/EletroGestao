using EletroGestao.Dominio.ProdutoRoot;

namespace EletroGestao.Application.Interfaces.Produtos
{
    public interface IProdutoService : IDisposable
    {
        Task<bool> Registrar(Produto produto);
    }
}
