using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace EletroGestao.Application.ViewModels.Pedido
{
    public class PedidoAdicionarViewModel
    {
        [DisplayName("Planilha")]
        [Required(ErrorMessage = "{0} é requerido")]
        public IFormFile Planilha { get; set; }
    }
}
