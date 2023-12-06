using AutoMapper;
using EletroGestao.Application.ViewModels.Pedido;
using EletroGestao.Dominio.PedidoRoot;

namespace EletroGestao.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Pedido, PedidoAdicionarViewModel>().ReverseMap();
            CreateMap<Pedido, PedidoAtualizarViewModel>().ReverseMap();
            CreateMap<Pedido, PedidoViewModel>().ReverseMap();
            CreateMap<PedidoTotais, PedidoTotaisViewModel>().ReverseMap();
        }
    }
}
