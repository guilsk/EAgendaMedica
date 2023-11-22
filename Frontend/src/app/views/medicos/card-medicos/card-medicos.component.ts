import { Component, Input } from '@angular/core';
import { ListarMedicoViewModel } from '../models/listar-medico.view-model';

@Component({
  selector: 'app-card-medicos',
  templateUrl: './card-medicos.component.html',
  styleUrls: ['./card-medicos.component.css']
})
export class CardMedicosComponent {
  @Input({ required: true }) medico!: ListarMedicoViewModel
}
