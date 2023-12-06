import { Routes } from '@angular/router'
import { AnalyticsComponent } from './pages/analytics/analytics.component'
import { UploadComponent } from './pages/upload/upload.component'

export const PrivateRoutes: Routes = [
    { path: 'analytics', component: AnalyticsComponent},
    { path: 'upload', component: UploadComponent},
]
