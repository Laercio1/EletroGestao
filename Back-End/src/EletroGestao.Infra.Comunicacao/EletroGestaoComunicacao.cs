using EletroGestao.Infra.Comunicacao.Models;
using System.Text.Json;

namespace EletroGestao.Infra.Comunicacao
{
    public static class EletroGestaoComunicacao
    {
        public static Localizacao ObterInformacoesLocalizacao(string cep)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/").Result)
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        return JsonSerializer.Deserialize<Localizacao>(content);
                    }
                }
                catch (Exception) { throw; }
            }
        }
    }
}
