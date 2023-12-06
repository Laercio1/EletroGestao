namespace EletroGestao.Application.ViewModels.Pedido
{
    public class PedidoTotaisViewModel
    {

        #region Norte/Nordeste
        public int PedidosTotalRegiaoNorteNordeste { get; set; }
        public decimal PedidosValorTotalRegiaoNorteNordeste { get; set; }
        #endregion

        #region Centro-oeste/Sul
        public int PedidosTotalRegiaoCentroOesteSul { get; set; }
        public decimal PedidosValorTotalRegiaoCentroOesteSul { get; set; }
        #endregion

        #region Sudeste
        public int PedidosTotalRegiaoSudeste { get; set; }
        public decimal PedidosValorTotalRegiaoSudeste { get; set; }
        #endregion

        #region São Paulo Capital
        public int PedidosTotalRegiaoSaoPauloCapital { get; set; }
        public decimal PedidosValorTotalRegiaoSaoPauloCapital { get; set; }
        #endregion

        #region Celular
        public int PedidosTotalCelular { get; set; }
        public decimal PedidosValorTotalCelular { get; set; }
        #endregion

        #region Notebook
        public int PedidosTotalNotebook { get; set; }
        public decimal PedidosValorTotalNotebook { get; set; }
        #endregion

        #region Televisão
        public int PedidosTotalTelevisao { get; set; }
        public decimal PedidosValorTotalTelevisao { get; set; }
        #endregion
    }
}
