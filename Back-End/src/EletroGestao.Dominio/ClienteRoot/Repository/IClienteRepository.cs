using EletroGestao.Dominio.Interfaces;

namespace EletroGestao.Dominio.ClienteRoot.Repository
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Cliente ObterCliente(string documento);
        Cliente VerificaClienteExiste(string documento);
    }
}
