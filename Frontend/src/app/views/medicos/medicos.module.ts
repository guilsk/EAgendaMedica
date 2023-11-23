import { NgModule } from "@angular/core";
import { InserirMedicosComponent } from "./inserir-medicos/inserir-medicos.component";
import { ListarMedicosComponent } from "./listar-medicos/listar-medicos.component";
import { CardMedicosComponent } from "./card-medicos/card-medicos.component";
import { EditarMedicosComponent } from "./editar-medicos/editar-medicos.component";
import { ExcluirMedicosComponent } from "./excluir-medicos/excluir-medicos.component";
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";
import { MedicosRoutingModule } from "./medicos-routing.module";
import { MedicosService } from "./services/medicos.service";
import { HttpClientModule } from "@angular/common/http";

@NgModule({
    declarations: [InserirMedicosComponent, ListarMedicosComponent, CardMedicosComponent, EditarMedicosComponent, ExcluirMedicosComponent],
    imports: [CommonModule, MedicosRoutingModule, ReactiveFormsModule, HttpClientModule],
    providers: [MedicosService]
})
export class MedicosModule {}