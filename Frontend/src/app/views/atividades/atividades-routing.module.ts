import { NgModule, inject } from "@angular/core";
import { ActivatedRouteSnapshot, ResolveFn, RouterModule, Routes } from '@angular/router';
import { ListarAtividadeViewModel } from "./models/listar-atividade.view-model";
import { AtividadesService } from "./services/atividades.service";
import { FormsAtividadeViewModel } from "./models/forms-atividade.view-model";
import { VisualizarAtividadeViewModel } from "./models/visualizar-atividade.view-model";
import { ListarAtividadesComponent } from "./listar-atividades/listar-atividades.component";
import { InserirAtividadesComponent } from "./inserir-atividades/inserir-atividades.component";
import { ExcluirAtividadesComponent } from "./excluir-atividades/excluir-atividades.component";
import { EditarAtividadesComponent } from "./editar-atividades/editar-atividades.component";
import { listarMedicosResolver } from "../medicos/medicos-routing.module";

const listarAtividadesResolver: ResolveFn<ListarAtividadeViewModel[]> = () => {
    return inject(AtividadesService).selecionarTodos()
}

const formsAtividadeResolver: ResolveFn<FormsAtividadeViewModel> = (
    route: ActivatedRouteSnapshot
) => {
    return inject(AtividadesService).selecionarPorId(route.paramMap.get('id')!)
}

const visualizarAtividadeResolver: ResolveFn<VisualizarAtividadeViewModel> = (
    route: ActivatedRouteSnapshot
) => {
    return inject(AtividadesService).selecionarAtividadeCompletoPorId(
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
        component: ListarAtividadesComponent,
        resolve: { atividades: listarAtividadesResolver }
    },
    {
        path: 'inserir',
        component: InserirAtividadesComponent
    },
    {
        path: 'editar/:id',
        component: EditarAtividadesComponent,
        resolve: { atividade: formsAtividadeResolver, medicos: listarMedicosResolver }
    },
    {
        path: 'excluir/:id',
        component: ExcluirAtividadesComponent,
        resolve: { atividade: visualizarAtividadeResolver }
    }
]

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AtividadesRoutingModule {}