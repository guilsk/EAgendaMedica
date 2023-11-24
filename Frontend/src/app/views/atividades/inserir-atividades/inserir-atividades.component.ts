import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ListarAtividadeViewModel } from '../models/listar-atividade.view-model';
import { AtividadesService } from '../services/atividades.service';
import { MedicosService } from '../../medicos/services/medicos.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { FormsAtividadeViewModel } from '../models/forms-atividade.view-model';
import { ListarMedicoViewModel } from '../../medicos/models/listar-medico.view-model';

@Component({
  selector: 'app-inserir-atividades',
  templateUrl: './inserir-atividades.component.html',
  styleUrls: ['./inserir-atividades.component.css']
})
export class InserirAtividadesComponent implements OnInit{
  form?: FormGroup
  medicos!: ListarMedicoViewModel[]
  atividades: ListarAtividadeViewModel[] = []

  constructor(
    private atividadesService: AtividadesService,
    public medicosService: MedicosService,
    private toastrService: ToastrService,
    private formBuilder: FormBuilder,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      descricao: new FormControl('', [Validators.required, /* Validators.minLength(3) */]),
      data: new FormControl('', [Validators.required]),
      horaInicio: new FormControl('', [Validators.required]),
      horaFim: new FormControl('', [Validators.required]),
      tipoAtividade: new FormControl(0, [Validators.required]),
      medicosId: new FormControl([], [Validators.required]),
    })

    this.atividadesService.selecionarTodos().subscribe((res) => (this.atividades = res))
    this.carregarMedicos()
  }

  gravar(){
    if(this.form?.invalid){
      for(let erro of this.form.validate()){
        this.toastrService.warning(erro)
      }
      return
    }

    this.atividadesService.inserir(this.form?.value).subscribe({
      next: (atividadeInserida) => this.processarSucesso(atividadeInserida),
      error: (err) => this.processarFalha(err)
    })
  }

  processarSucesso(atividade: FormsAtividadeViewModel){
    this.toastrService.success(`A atividade "${atividade.descricao}" foi cadastrada com sucesso!`, 'Sucesso')
    this.router.navigate(['/atividades/listar'])
  }

  processarFalha(err: Error){
    this.toastrService.error(err.message, 'Error')
  }

  carregarMedicos(){
    this.medicosService.selecionarTodos().subscribe((medicos: ListarMedicoViewModel[]) => {
      this.medicos = medicos
    })
  }

}
