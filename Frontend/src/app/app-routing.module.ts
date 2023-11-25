import { NgModule } from '@angular/core';
import { RouterModule, Routes, } from '@angular/router';
import { DashboardComponent } from './views/dashboard/dashboard.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  },
  {
    path: 'dashboard',
    component: DashboardComponent,
  },
  {
    path: 'medicos',
    loadChildren: () => import('./views/medicos/medicos.module').then((m) => m.MedicosModule),
  },
  {
    path: 'atividades',
    loadChildren: () => import('./views/atividades/atividades.module').then((a) => a.AtividadesModule),
  },
  {
    path: 'dashboard',
    loadChildren: () => import('./views/dashboard/dashboard.module').then((d) => d.DashboardModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}