import { HttpHeaders, HttpErrorResponse } from '@angular/common/http'

import { throwError } from 'rxjs'
import { environment } from 'src/environments/environment'

export abstract class BaseService {

  protected UrlServiceV1: string = environment.baseUrl

  protected ObterHeaderJson() {
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    }
  }

  protected extractData(response: any) {
    return response.data || {}
  }

  protected serviceError(response: Response | any) {
    const customErro: string[] = []
    const erros: string[] = []
    const customResponse = { erro: { erros } }

    if (response instanceof HttpErrorResponse) {
      if (response.statusText === 'Unknown Error') {
        customErro.push('Ocorreu um erro desconhecido')
        response.error.errors = customErro
      }
    }

    if (response.status === 500) {
      customErro.push('Ocorreu um erro no processamento, tente novamente mais tarde ou contate o nosso suporte.')
      // Erros do tipo 500 não possuem uma lista de erros
      // A lista de erros do HttpErrorResponse é readonly
      customResponse.erro.erros = customErro
      return throwError(customResponse)
    }

    return throwError(response)
  }
}
