using EletroGestao.Application.ViewModels.Base;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EletroGestao.Application.ViewModels.Pedido
{
    public class PedidoAtualizarViewModel : BaseViewModelCadastro
    {
        [DisplayName("Planilha")]
        [Required(ErrorMessage = "{0} é requerido")]
        public IFormFile Planilha { get; set; }
    }
}
