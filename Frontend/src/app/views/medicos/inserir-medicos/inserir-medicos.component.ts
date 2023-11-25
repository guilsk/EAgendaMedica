import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { FormsMedicoViewModel } from '../models/forms-medico.view-model';
import { MedicosService } from '../services/medicos.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-inserir-medicos',
  templateUrl: './inserir-medicos.component.html',
  styleUrls: ['./inserir-medicos.component.css']
})
export class InserirMedicosComponent implements OnInit{
  form!: FormGroup
  medicoVM!: FormsMedicoViewModel

  constructor(private formBuilder: FormBuilder, private medicosService: MedicosService, private toastrService: ToastrService, private router: Router){}

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      nome: new FormControl('', [Validators.required]),
      crm: new FormControl('', [Validators.required, /*Validators.crm*/])
    })
  }

  campoEstaInvalido(nome: string){
    return this.form.get(nome)!.touched && this.form.get(nome)!.invalid
  }

  get crm(){
    return this.form.get('crm')
  }

  gravar(){
    if(this.form.invalid){
      for (let erro of this.form.validate()){
        this.toastrService.warning(erro)
      }
      return
    }

    this.medicosService.inserir(this.form?.value).subscribe({
      next: () => this.processarSucesso(this.form?.value),
      error: (err) => this.processarFalha(err)
    })
  }

  processarSucesso(medico: FormsMedicoViewModel){
    this.toastrService.success(`O médico "${medico.nome}" foi cadastrado com sucesso!`, 'Sucesso')
    this.router.navigate(['/medicos/listar'])
  }

  processarFalha(err: HttpErrorResponse){
    const mensagemErro = err.error.erros.length > 0 ? err.error.erros[0] : 'Ocorreu um erro desconhecido.';
    this.toastrService.error(mensagemErro)
  }

}
