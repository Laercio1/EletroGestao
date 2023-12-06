namespace EletroGestao.API.Configuration
{
    public class AppSettings
    {
        //Chave de criptografia
        public string Secret { get; set; }
        //Quantas horas até o token perder a validade
        public int Expiration { get; set; }
        //Nome da aplicação que emite
        public string Issuer { get; set; }
        //Em quais urls que o token é válido
        public string ValidAt { get; set; }
    }
}
