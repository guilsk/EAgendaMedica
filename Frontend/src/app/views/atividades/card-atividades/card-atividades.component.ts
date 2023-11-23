import { Component, Input } from '@angular/core';
import { ListarAtividadeViewModel } from '../models/listar-atividade.view-model';
import { TipoAtividadeEnum } from '../models/tipo-atividade.enum';

@Component({
  selector: 'app-card-atividades',
  templateUrl: './card-atividades.component.html',
  styleUrls: ['./card-atividades.component.css']
})
export class CardAtividadesComponent {
  @Input({ required: true }) atividade!: ListarAtividadeViewModel

  getTipoAtividade(tipoAtividade: number): string{
    return TipoAtividadeEnum[tipoAtividade]
  }
}
