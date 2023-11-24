import { NgModule, inject } from "@angular/core";
import { ActivatedRouteSnapshot, ResolveFn, RouterModule, Routes } from '@angular/router';
import { ListarMedicoViewModel } from "./models/listar-medico.view-model";
import { MedicosService } from "./services/medicos.service";
import { FormsMedicoViewModel } from "./models/forms-medico.view-model";
import { VisualizarMedicoViewModel } from "./models/visualizar-medico.view-model";
import { ListarMedicosComponent } from "./listar-medicos/listar-medicos.component";
import { InserirMedicosComponent } from "./inserir-medicos/inserir-medicos.component";
import { ExcluirMedicosComponent } from "./excluir-medicos/excluir-medicos.component";
import { EditarMedicosComponent } from "./editar-medicos/editar-medicos.component";

export const listarMedicosResolver: ResolveFn<ListarMedicoViewModel[]> = () => {
    return inject(MedicosService).selecionarTodos()
}

const formsMedicoResolver: ResolveFn<FormsMedicoViewModel> = (
    route: ActivatedRouteSnapshot
) => {
    return inject(MedicosService).selecionarPorId(route.paramMap.get('id')!)
}

const visualizarMedicoResolver: ResolveFn<VisualizarMedicoViewModel> = (
    route: ActivatedRouteSnapshot
) => {
    return inject(MedicosService).selecionarMedicoCompletoPorId(
        route.paramMap.get('id')!
    )
}

const routes: Routes = [
    {
        path: '',
        redirectTo: 'listar',
        pathMatch: 'full'
    },
    {
        path: 'listar',
        component: ListarMedicosComponent,
        resolve: { medicos: listarMedicosResolver }
    },
    {
        path: 'inserir',
        component: InserirMedicosComponent
    },
    {
        path: 'editar/:id',
        component: EditarMedicosComponent,
        resolve: { medico: formsMedicoResolver }
    },
    {
        path: 'excluir/:id',
        component: ExcluirMedicosComponent,
        resolve: { medico: visualizarMedicoResolver }
    }
]

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class MedicosRoutingModule {}