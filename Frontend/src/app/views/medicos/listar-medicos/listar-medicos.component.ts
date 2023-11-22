import { Component, OnInit } from '@angular/core';
import { ListarMedicoViewModel } from '../models/listar-medico.view-model';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs';

@Component({
  selector: 'app-listar-medicos',
  templateUrl: './listar-medicos.component.html',
  styleUrls: ['./listar-medicos.component.css']
})
export class ListarMedicosComponent implements OnInit {
  medicos: ListarMedicoViewModel[] = []

  constructor(private route: ActivatedRoute, private toastrService: ToastrService){}

  ngOnInit(): void {
    this.route.data.pipe(map((dados) => dados['medicos'])).subscribe({
      next: (medicos) => this.obterMedicos(medicos),
      error: (erro) => this.processarFalha(erro)
    })
  }

  obterMedicos(medicos: ListarMedicoViewModel[]) {
    this.medicos = medicos;
  }

  processarFalha(erro: Error) {
    this.toastrService.error(erro.message, 'Erro');
  }
}
