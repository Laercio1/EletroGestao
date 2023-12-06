import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { RouterModule } from '@angular/router'
import { FormsModule, ReactiveFormsModule } from '@angular/forms'

import { PublicComponent } from './public.component'
import { PublicRoutes } from './public.routing'

@NgModule({
  declarations: [
    PublicComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(PublicRoutes),
    FormsModule,
    FormsModule,
    ReactiveFormsModule,
  ]
})
export class PublicModule { }
