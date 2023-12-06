using EletroGestao.Dominio.ClienteRoot;

namespace EletroGestao.Application.Interfaces.Clientes
{
    public interface IClienteService : IDisposable
    {
        Task<bool> Registrar(Cliente cliente);
    }
}
