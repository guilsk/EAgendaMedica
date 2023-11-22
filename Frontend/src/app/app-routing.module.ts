import { NgModule, inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivateFn,
  ResolveFn,
  Router,
  RouterModule,
  Routes,
  UrlTree,
} from '@angular/router';
import { DashboardComponent } from './views/dashboard/dashboard.component';
//import { authGuard } from './core/auth/guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'medicos',
    pathMatch: 'full',
  },
  {
    path: 'dashboard',
    component: DashboardComponent,
    //canActivate: [authGuard],
  },
  {
    path: 'medicos',
    loadChildren: () => import('./views/medicos/medicos.module').then((m) => m.MedicosModule),
    //canActivate: [authGuard],
  },
  {
    path: 'atividades',
    loadChildren: () => import('./views/atividades/atividades.module').then((a) => a.AtividadesModule),
    //canActivate: [authGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}