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
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [InserirAtividadesComponent, ListarAtividadesComponent, CardAtividadesComponent, EditarAtividadesComponent, ExcluirAtividadesComponent],
  imports: [CommonModule, AtividadesRoutingModule, ReactiveFormsModule, HttpClientModule, CommonModule],
  providers: [AtividadesService]

})
export class AtividadesModule { }
