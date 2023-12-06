import { Component, OnInit } from '@angular/core'
import { UploadService } from '../../services/upload.service';

import { ToastrService } from 'ngx-toastr'
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner'
import { PlanilhaService } from 'src/app/shared/services/planilhaService.service';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {

  constructor(private planilhaService: PlanilhaService,
    private uploadService: UploadService,
    public toastr: ToastrService,
    private router: Router,
    private spinner: NgxSpinnerService,
  ) { }

  ngOnInit (): void {
  }

  downloadPlanilha(): void {
    this.planilhaService.downloadPlanilha();
  }

  Upload(files: FileList): void {
    const file = files.item(0);

    if (file && file.type === 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet') {
      this.spinner.show()
      this.uploadService.upload(file)
      .subscribe(
        () => this.processoSuccesso(),
        () => this.processoErro()
      ).add(() => this.spinner.hide())
    } else {
      this.toastr.warning('Selecione um arquivo no formato Excel (.xlsx).');
    }
  }

  private processoSuccesso(): void {
    this.toastr.success('Upload realizado com sucesso!')
    this.router.navigate(['/analytics'])
  }

  private processoErro(): void {
    this.toastr.error('Erro ao tentar realizar upload!')
  }
}
