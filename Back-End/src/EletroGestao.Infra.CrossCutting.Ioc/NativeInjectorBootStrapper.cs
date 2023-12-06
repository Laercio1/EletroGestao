using EletroGestao.Application.Interfaces.Clientes;
using EletroGestao.Application.Interfaces.Pedidos;
using EletroGestao.Application.Interfaces.Produtos;
using EletroGestao.Application.Notificacoes;
using EletroGestao.Application.Services.Clientes;
using EletroGestao.Application.Services.Pedidos;
using EletroGestao.Application.Services.Produtos;
using EletroGestao.Dominio.ClienteRoot.Repository;
using EletroGestao.Dominio.PedidoRoot.Repository;
using EletroGestao.Dominio.ProdutoRoot.Repository;
using EletroGestao.Infra.Data.Context;
using EletroGestao.Infra.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EletroGestao.Infra.CrossCutting.Ioc
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<EletroGestaoContext>();

            services.AddScoped<HttpClient>();

            //services.AddScoped<IMediator, Mediator>();

            // ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<INotificador, Notificador>();

            #region Services

            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IProdutoService, ProdutoService>();

            #endregion

            #region Repositories

            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            #endregion

            // services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        }
    }
}