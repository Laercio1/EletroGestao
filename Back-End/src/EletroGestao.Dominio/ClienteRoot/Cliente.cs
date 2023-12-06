using EletroGestao.Dominio.Core.Annotations;
using EletroGestao.Dominio.Core.Models;

namespace EletroGestao.Dominio.ClienteRoot
{
    [Table("Clientes")]
    public class Cliente : Entity
    {
        public DateTime DataCadastro { get; set; }

        public string NomeRazaoSocial { get; set; }

        public string Documento { get; set; }
    }
}
