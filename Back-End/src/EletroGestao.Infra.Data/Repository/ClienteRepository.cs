using EletroGestao.Dominio.ClienteRoot;
using EletroGestao.Dominio.ClienteRoot.Repository;
using EletroGestao.Infra.Data.Context;
using EletroGestao.Infra.Data.Repository.Base;

namespace EletroGestao.Infra.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(EletroGestaoContext context) : base(context)
        {

        }

        public Cliente ObterCliente(string documento)
        {
            var cliente = Db.Cliente.Where(p => p.Documento == documento.Replace(".", "").Replace("-", "").Replace("/", "")).FirstOrDefault();

            return cliente;
        }

        public Cliente VerificaClienteExiste(string documento)
        {
            return Db.Cliente.Where(p => p.Documento == documento.Replace(".", "").Replace("-", "").Replace("/", "")).FirstOrDefault();
        }
    }
}
