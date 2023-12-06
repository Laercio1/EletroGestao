import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { RouterModule } from '@angular/router'
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { HttpClientModule } from '@angular/common/http'

import { PrivateRoutes } from './private.routing'
import { PrivateComponent } from './private.component'
import { AnalyticsComponent } from './pages/analytics/analytics.component'
import { UploadComponent } from './pages/upload/upload.component'
import { NgxSpinnerModule } from 'ngx-spinner'
import { PaginationModule } from 'ngx-bootstrap/pagination'

@NgModule({
  declarations: [
    PrivateComponent,
    AnalyticsComponent,
    UploadComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(PrivateRoutes),
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgxSpinnerModule,
    PaginationModule.forRoot(),
  ],
})
export class PrivateModule { }
