using EletroGestao.Application.Interfaces.Clientes;
using EletroGestao.Application.Notificacoes;
using EletroGestao.Application.Services.Base;
using EletroGestao.Dominio.ClienteRoot;
using EletroGestao.Dominio.ClienteRoot.Repository;
using EletroGestao.Dominio.ClienteRoot.Validation;

namespace EletroGestao.Application.Services.Clientes
{
    public class ClienteService : BaseService, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(INotificador notificador,
                                IClienteRepository repository) : base(notificador)
        {
            _clienteRepository = repository;
        }

        public void Dispose()
        {
            _clienteRepository.Dispose();
        }

        public async Task<bool> Registrar(Cliente cliente)
        {
            ExecutarValidacao(new ClienteValidation(), cliente);

            if (_notificador.TemNotificacao())
                return false;

            try
            {
                await _clienteRepository.Adicionar(cliente);
            }
            catch (Exception ex)
            {
                Notificar(string.Format("Não foi possível adicionar Cliente. Motivo: {0}", ex.Message));
            }

            return !_notificador.TemNotificacao();
        }
    }
}
