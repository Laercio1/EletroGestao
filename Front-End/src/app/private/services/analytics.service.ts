import { Injectable } from '@angular/core'
import { HttpClient, HttpParams } from '@angular/common/http'
import { Observable } from 'rxjs';
import { ITotaisVendas } from '../interfaces/totaisVendas';
import { catchError, map, take } from 'rxjs/operators'
import { BaseService } from 'src/app/core/services/base.service';
import { IVendas } from '../interfaces/vendas';

@Injectable({
  providedIn: 'root'
})
export class AnalyticsService extends BaseService  {

    constructor(private http: HttpClient) { super() }

  getTotaisPedido (): Observable<ITotaisVendas> {

    const response = this.http
      .get<ITotaisVendas>(this.UrlServiceV1 + 'pedido/totais-pedidos')
      .pipe(
        map(this.extractData),
        catchError(this.serviceError))

    return response
  }

  getVendas (route: string, page: number, pageSize: number): Observable<IVendas> {

    let params = new HttpParams

    if (page !== null && pageSize !== null) {
      params = params.append('pagina', page.toString())
      params = params.append('tamanhoPagina', pageSize.toString())
    }

    const response = this.http
      .get<IVendas>(this.UrlServiceV1 + route + params)
      .pipe(
        map(this.extractData),
        catchError(this.serviceError),
        take(1))

    return response
  }

}
