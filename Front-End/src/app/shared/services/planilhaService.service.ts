import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PlanilhaService {

  constructor(private http: HttpClient) { }

  downloadPlanilha(): void {
    const planilhaPath = 'assets/modelo-planilha.xlsx';

    this.http.get(planilhaPath, { responseType: 'arraybuffer' })
      .subscribe((data: ArrayBuffer) => {
        const blob = new Blob([data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
        const url = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = url;
        link.download = 'modelo planilha.xlsx';
        link.click();
        window.URL.revokeObjectURL(url);
      });
  }
}
