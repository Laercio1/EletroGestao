import { NgModule } from '@angular/core'
import { RouterModule, Routes } from '@angular/router'
import { PublicComponent } from './public/public.component'
import { PrivateComponent } from './private/private.component'

const routes: Routes = [
  { path: '', redirectTo: 'upload', pathMatch: 'full' },
  {
    path: '', component: PublicComponent,
    children: [
      {
        path: '',
        loadChildren: () => import('./public/public.module').then(m => m.PublicModule)
      }
    ]
  },
  {
    path: '', component: PrivateComponent,
    children: [
      {
        path: '',
        loadChildren: () => import('./private/private.module').then(m => m.PrivateModule)
      }
    ]
  }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
