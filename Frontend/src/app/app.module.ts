import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NavbarComponent } from './core/navbar/navbar.component';
import { MedicosComponent } from './views/medicos/medicos.component';
import { AtividadesComponent } from './views/atividades/atividades.component';
import { ListarMedicosComponent } from './views/medicos/listar-medicos/listar-medicos.component';
import { CardMedicosComponent } from './views/medicos/card-medicos/card-medicos.component';
import { EditarMedicosComponent } from './views/medicos/editar-medicos/editar-medicos.component';
import { ExcluirMedicosComponent } from './views/medicos/excluir-medicos/excluir-medicos.component';
import { InserirMedicosComponent } from './views/medicos/inserir-medicos/inserir-medicos.component';
import { ListarAtividadesComponent } from './views/atividades/listar-atividades/listar-atividades.component';
import { CardAtividadesComponent } from './views/atividades/card-atividades/card-atividades.component';
import { EditarAtividadesComponent } from './views/atividades/editar-atividades/editar-atividades.component';
import { ExcluirAtividadesComponent } from './views/atividades/excluir-atividades/excluir-atividades.component';
import { InserirAtividadesComponent } from './views/atividades/inserir-atividades/inserir-atividades.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    MedicosComponent,
    AtividadesComponent,
    ListarMedicosComponent,
    CardMedicosComponent,
    EditarMedicosComponent,
    ExcluirMedicosComponent,
    InserirMedicosComponent,
    ListarAtividadesComponent,
    CardAtividadesComponent,
    EditarAtividadesComponent,
    ExcluirAtividadesComponent,
    InserirAtividadesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
