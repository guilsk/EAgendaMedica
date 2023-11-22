import { Component, OnInit } from '@angular/core';
import { VisualizarMedicoViewModel } from '../models/visualizar-medico.view-model';
import { MedicosService } from '../services/medicos.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs';

@Component({
  selector: 'app-excluir-medicos',
  templateUrl: './excluir-medicos.component.html',
  styleUrls: ['./excluir-medicos.component.css']
})
export class ExcluirMedicosComponent implements OnInit{
  medicoVM!: VisualizarMedicoViewModel

  constructor(private medicosService: MedicosService, private toastrService: ToastrService, private route: ActivatedRoute, private router: Router){
  }

  ngOnInit(): void {
    this.route.data.pipe(map((dados) => dados['medico'])).subscribe({
      next: (medico) => this.obterMedico(medico),
      error: (erro) => this.processarFalha(erro)
    })
  }

  gravar(){
    this.medicosService.excluir(this.medicoVM.id).subscribe({
      next: () => this.processarSucesso(),
      error: (erro) => this.processarFalha(erro)
    })
  }

  obterMedico(medico: VisualizarMedicoViewModel){
    this.medicoVM = medico
  }

  processarSucesso(){
    this.toastrService.success(`O médico foi excluído com sucesso!`, 'Sucesso')
    this.router.navigate(['/medicos', 'listar'])
  }

  processarFalha(erro: Error){
    this.toastrService.error(erro.message, 'Error')
  }

}
