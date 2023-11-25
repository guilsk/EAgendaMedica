import { Component, Inject, LOCALE_ID } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { FormsAtividadeViewModel } from '../models/forms-atividade.view-model';
import { AtividadesService } from '../services/atividades.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, map } from 'rxjs';
import { ListarMedicoViewModel } from '../../medicos/models/listar-medico.view-model';
import {formatDate} from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-editar-atividades',
  templateUrl: './editar-atividades.component.html',
  styleUrls: ['./editar-atividades.component.css']
})
export class EditarAtividadesComponent {
  form!: FormGroup
  medicos!: Observable<ListarMedicoViewModel[]>
  atividadeVM!: FormsAtividadeViewModel

  constructor(private formBuilder: FormBuilder, 
    private atividadesService: AtividadesService, 
    private toastrService: ToastrService, 
    private router: Router, 
    private route: ActivatedRoute, 
    @Inject(LOCALE_ID) private locale: string) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      descricao: new FormControl('', [Validators.required]),
      data: new FormControl('', [Validators.required]),
      horaInicio: new FormControl('', [Validators.required]),
      horaFim: new FormControl('', [Validators.required]),
      tipoAtividade: new FormControl(0, [Validators.required]),
      medicosId: new FormControl([], [Validators.required]),
    })

    this.carregarMedicos()
    this.route.data.pipe(map((dados) => dados['atividade'])).subscribe({
      next: (atividade) => {
        const formsAtividade = this.atividadesService.converterVisualizarEmForms(atividade)
        this.obterAtividade(formsAtividade)
      },
      error: (erro) => this.processarFalha(erro)
    })
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
      next: () => this.processarSucesso(this.atividadeVM),
      error: (erro) => this.processarFalha(erro)
    })
  }

  obterAtividade(atividade: FormsAtividadeViewModel){
    this.atividadeVM = atividade
    this.form.patchValue({...this.atividadeVM, data: formatDate(this.atividadeVM.data, 'yyyy-MM-dd', this.locale), medicosId: this.atividadeVM.medicosId})
  }

  processarSucesso(atividade: FormsAtividadeViewModel){
    this.toastrService.success(`A atividade "${atividade.descricao}" foi editada com sucesso!`, 'Sucesso')
    this.router.navigate(['/atividades/listar'])
  }
  
  processarFalha(err: HttpErrorResponse){
    const mensagemErro = err.error.erros.length > 0 ? err.error.erros[0] : 'Ocorreu um erro desconhecido.';
    this.toastrService.error(mensagemErro)
  }

  carregarMedicos(){
    this.medicos = this.route.data.pipe(map((dados) => dados['medicos']))
  }

}
