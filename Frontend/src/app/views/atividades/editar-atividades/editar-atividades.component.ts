import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { FormsAtividadeViewModel } from '../models/forms-atividade.view-model';
import { AtividadesService } from '../services/atividades.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs';
import { ListarMedicoViewModel } from '../../medicos/models/listar-medico.view-model';
import { MedicosService } from '../../medicos/services/medicos.service';

@Component({
  selector: 'app-editar-atividades',
  templateUrl: './editar-atividades.component.html',
  styleUrls: ['./editar-atividades.component.css']
})
export class EditarAtividadesComponent {
  form!: FormGroup
  medicos!: ListarMedicoViewModel[]
  atividadeVM!: FormsAtividadeViewModel

  constructor(private formBuilder: FormBuilder, private atividadesService: AtividadesService, private medicosService: MedicosService, private toastrService: ToastrService, private router: Router, private route: ActivatedRoute){
  }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      descricao: new FormControl('', [Validators.required]),
      data: new FormControl('', [Validators.required]),
      horaInicio: new FormControl('', [Validators.required]),
      horaFim: new FormControl('', [Validators.required]),
      tipoAtividade: new FormControl(0, [Validators.required]),
      medicosId: new FormControl([], [Validators.required]),
    })

    this.route.data.pipe(map((dados) => dados['atividades'])).subscribe({
      next: (atividade) => this.obterAtividade(atividade),
      error: (erro) => this.processarFalha(erro)
    })
    this.carregarMedicos()
  }

  gravar(){
    if(this.form.invalid){
      for(let erro of this.form.validate()){
        this.toastrService.warning(erro)
      }
      return
    }

    this.atividadeVM = this.form.value

    const id = this.route.snapshot.paramMap.get('id')

    if(!id) return

    this.atividadesService.editar(id, this.atividadeVM).subscribe({
      next: (atividade) => this.processarSucesso(atividade),
      error: (erro) => this.processarFalha(erro)
    })
  }

  obterAtividade(atividade: FormsAtividadeViewModel){
    this.atividadeVM = atividade
    this.form.patchValue(this.atividadeVM)
  }

  processarSucesso(atividade: FormsAtividadeViewModel){
    this.toastrService.success(`A atividade "${atividade.descricao}" foi editada com sucesso!`, 'Sucesso')
    this.router.navigate(['/atividades/listar'])
  }
  
  processarFalha(erro: Error){
    this.toastrService.error(erro.message, 'Error')
  }

  carregarMedicos(){
    this.medicosService.selecionarTodos().subscribe((medicos: ListarMedicoViewModel[]) => {
      this.medicos = medicos
    })
  }

}
