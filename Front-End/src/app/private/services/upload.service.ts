import { Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/core/services/base.service';
import { catchError, map, take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UploadService extends BaseService  {

    constructor(private http: HttpClient) { super() }

  upload (file: File): Observable<any> {
    
    const formData = new FormData();
    formData.append('planilha', file, file.name);

    const response = this.http
      .post<any>(this.UrlServiceV1 + 'pedido/upload', formData)
      .pipe(
        map(this.extractData),
        catchError(this.serviceError),
        take(1))

    return response
  }
}
