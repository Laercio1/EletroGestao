namespace EletroGestao.Dominio
{
    public class RetornoSucesso
    {
        public bool success { get; set; }
        public List<Mensagem> data { get; set; }
    }

    public class Mensagem
    {
        public string mensagem { get; set; }
    }
}
