using EletroGestao.Dominio.Core.Annotations;
using EletroGestao.Dominio.Core.Models;

namespace EletroGestao.Dominio.ProdutoRoot
{
    [Table("Produtos")]
    public class Produto : Entity
    {
        public DateTime DataCadastro { get; set; }

        public string NomeProduto { get; set; }

        public decimal Valor { get; set; }
    }
}
