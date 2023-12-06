import { Component, ElementRef, OnInit } from '@angular/core'
import * as Chart from 'chart.js'
import { AnalyticsService } from '../../services/analytics.service'
import { IRegioes } from '../../interfaces/regioes'
import { ITotaisVendas } from '../../interfaces/totaisVendas'
import { IProduto } from '../../interfaces/produto'
import { NOMES_ITENS, NomesItens } from 'src/app/utils/nomesItens'
import { IVendas, IVendasListaRetorno } from '../../interfaces/vendas'
import { IPagination } from '../../interfaces/pagination'
import { ToastrService } from 'ngx-toastr'

@Component({
  selector: 'app-analytics',
  templateUrl: './analytics.component.html',
  styleUrls: ['./analytics.component.css']
})
export class AnalyticsComponent implements OnInit {

  htmlReference = ''
  pieChartRegiao!: Chart
  pieChartProduto!: Chart
  regioesTotais: IRegioes[] = []
  produtosTotais: IProduto[] = []

  nomes!: NomesItens[]

  pedidosList!: IVendasListaRetorno[]
  pagination = {} as IPagination

  constructor (
    private analyticsService: AnalyticsService,
    private elementReference: ElementRef,
    public toastr: ToastrService
  ) { }

  ngOnInit (): void {
    this.nomes = NOMES_ITENS.filter((nomesItens: any) => nomesItens)
    this.pagination = {
      paginaAtual: 1,
      tamanhoPagina: 10,
      totalPaginas: 1,
      totalItens: 1
    }
    this.CarregarListaVendas()
  }

  ngAfterViewInit (): void {

        this.analyticsService.getTotaisPedido()
          .subscribe(totais => {

            this.regioesTotais = this.preencherArrayRegiao(totais)
            this.produtosTotais = this.preencherArrayProduto(totais)

            this.chartPieRegiao(this.regioesTotais)
            this.chartPieProduto(this.produtosTotais)
          })
}

private CarregarListaVendas() {
  this.analyticsService.getVendas(
    'pedido?',
    this.pagination.paginaAtual,
    this.pagination.tamanhoPagina
  ).subscribe(
    sucesso => this.processoSuccesso(sucesso),
    () => this.processoErro()
  )
}

  chartPieRegiao (regioesTotais: IRegioes[]) {
    const pie = '#chartPieRegiao'
    this.htmlReference = this.elementReference.nativeElement.querySelector(pie)
    this.pieChartRegiao = new Chart(this.htmlReference, {
      type: 'pie',
      data: {
        labels: ["Norte/Nordeste", "Centro-Oeste/Sul",  "Sudeste", "São Paulo Capital"],
        datasets: [{
          label: "",
          data: [regioesTotais[0].regiaoNorteNordesteValorTotal, regioesTotais[0].regiaoCentroOesteSulValorTotal,
          regioesTotais[0].regiaoSudesteValorTotal, regioesTotais[0].regiaoSaoPauloCapitalValorTotal],
          backgroundColor: [
            "rgba(255, 99, 132, 1)",
            "rgba(255, 206, 86, 1)",
            "rgba(160, 212, 104, 1)",
            "rgba(54, 162, 235, 1)"
          ],
        }],
      },
      options: {
        responsive: true,
      },
    })
  }

  chartPieProduto (produtosTotais: IProduto[]) {
    const pie = '#chartPieProduto'
    this.htmlReference = this.elementReference.nativeElement.querySelector(pie)
    this.pieChartProduto = new Chart(this.htmlReference, {
      type: 'pie',
      data: {
        labels: ["Celular", "Notebook",  "Televisão"],
        datasets: [{
          label: "",
          data: [produtosTotais[0].celularValorTotal, produtosTotais[0].notebookValorTotal, 
          produtosTotais[0].televisaoValorTotal],
          backgroundColor: [
            "rgba(255, 206, 86, 1)",
            "rgba(160, 212, 104, 1)",
            "rgba(54, 162, 235, 1)"
          ],
        }],
      },
      options: {
        responsive: true,
      },
    })
  }

  protected preencherArrayRegiao (totais: ITotaisVendas) {
    let array: IRegioes[] = []
    array = [
      {
        regiaoNorteNordesteTotal: Number(totais.pedidosTotalRegiaoNorteNordeste),
        regiaoNorteNordesteValorTotal: Number(totais.pedidosValorTotalRegiaoNorteNordeste),
        regiaoCentroOesteSulTotal: Number(totais.pedidosTotalRegiaoCentroOesteSul),
        regiaoCentroOesteSulValorTotal: Number(totais.pedidosValorTotalRegiaoCentroOesteSul),
        regiaoSudesteTotal: Number(totais.pedidosTotalRegiaoSudeste),
        regiaoSudesteValorTotal: Number(totais.pedidosValorTotalRegiaoSudeste),
        regiaoSaoPauloCapitalTotal: Number(totais.pedidosTotalRegiaoSaoPauloCapital),
        regiaoSaoPauloCapitalValorTotal: Number(totais.pedidosValorTotalRegiaoSaoPauloCapital),
      }
    ]

    return array
  }

  
  protected preencherArrayProduto (totais: ITotaisVendas) {
    let array: IProduto[] = []
    array = [
      {
        celularTotal: Number(totais.pedidosTotalCelular),
        celularValorTotal: Number(totais.pedidosValorTotalCelular),
        notebookTotal: Number(totais.pedidosTotalNotebook),
        notebookValorTotal: Number(totais.pedidosValorTotalNotebook),
        televisaoTotal: Number(totais.pedidosTotalTelevisao),
        televisaoValorTotal: Number(totais.pedidosValorTotalTelevisao)
      }
    ]

    return array
  }

  private processoSuccesso(pedidos: IVendas): void {
    this.pedidosList = pedidos.listaRetorno
    this.pagination.paginaAtual = pedidos.paginaAtual
    this.pagination.tamanhoPagina = pedidos.tamanhoPagina
    this.pagination.totalPaginas = pedidos.totalPaginas
  }

  private processoErro(): void {
    this.toastr.warning('Nenhuma venda foi encontrada!')
    this.pedidosList = []
    this.pagination.paginaAtual = 1
    this.pagination.tamanhoPagina = 25
    this.pagination.totalPaginas = 1
  }

  pageChanged(event: any): void {
    this.pagination.paginaAtual = event.page
    this.CarregarListaVendas()
  }
}
