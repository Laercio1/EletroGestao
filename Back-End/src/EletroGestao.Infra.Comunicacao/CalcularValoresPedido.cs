using EletroGestao.Dominio.PedidoRoot;
using EletroGestao.Infra.Comunicacao.Models;

namespace EletroGestao.Infra.Comunicacao
{
    public class CalcularValoresPedido
    {
        public static Pedido ObterValoresPedido(Localizacao localizacao, DateTime data, decimal valorProduto)
        {
            var regiao = ObterRegiao(localizacao);

            switch (regiao)
            {
                case "Norte/Nordeste":
                    return new Pedido
                    {
                        ValorFrete = valorProduto * 0.3m,
                        DataEntrega = AdicionarDias(data, 10),
                        Regiao = regiao
                    };
                case "Centro-oeste/Sul":
                    return new Pedido
                    {
                        ValorFrete = valorProduto * 0.2m,
                        DataEntrega = AdicionarDias(data, 5),
                        Regiao = regiao
                    };
                case "Sudeste":
                    return new Pedido
                    {
                        ValorFrete = valorProduto * 0.1m,
                        DataEntrega = AdicionarDias(data, 1),
                        Regiao = regiao
                    };
                case "São Paulo Capital":
                    return new Pedido
                    {
                        ValorFrete = 0,
                        DataEntrega = AdicionarDias(data, 0),
                        Regiao = regiao
                    };
                default:
                    return new Pedido();
            }
        }

        public static string ObterRegiao(Localizacao localizacao)
        {
            if (localizacao != null)
            {
                switch (localizacao.uf)
                {
                    case "SP" when localizacao.localidade == "São Paulo":
                        return "São Paulo Capital";
                    case "AC" or "AM" or "AP" or "PA" or "RO" or "RR" or "TO" or "AL" or "BA" or "CE" or "MA" or "PB" or "PE" or "PI" or "RN" or "SE":
                        return "Norte/Nordeste";
                    case "DF" or "GO" or "MT" or "MS" or "PR" or "RS" or "SC":
                        return "Centro-oeste/Sul";
                    case "ES" or "MG" or "RJ" or "SP":
                        return "Sudeste";
                    default:
                        return "Região não identificada";
                }
            }

            return "Região não identificada";
        }

        private static DateTime AdicionarDias(DateTime data, int dias)
        {
            return data.AddDays(dias);
        }
    }
}
