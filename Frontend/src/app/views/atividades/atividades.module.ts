import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InserirAtividadesComponent } from './inserir-atividades/inserir-atividades.component';
import { ListarAtividadesComponent } from './listar-atividades/listar-atividades.component';
import { CardAtividadesComponent } from './card-atividades/card-atividades.component';
import { EditarAtividadesComponent } from './editar-atividades/editar-atividades.component';
import { ExcluirAtividadesComponent } from './excluir-atividades/excluir-atividades.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AtividadesRoutingModule } from './atividades-routing.module';
import { AtividadesService } from './services/atividades.service';



@NgModule({
  declarations: [InserirAtividadesComponent, ListarAtividadesComponent, CardAtividadesComponent, EditarAtividadesComponent, ExcluirAtividadesComponent],
  imports: [CommonModule, AtividadesRoutingModule, ReactiveFormsModule],
  providers: [AtividadesService]

})
export class AtividadesModule { }
