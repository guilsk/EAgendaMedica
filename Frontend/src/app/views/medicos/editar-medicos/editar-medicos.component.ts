import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { FormsMedicoViewModel } from '../models/forms-medico.view-model';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { MedicosService } from '../services/medicos.service';
import { map } from 'rxjs';

@Component({
  selector: 'app-editar-medicos',
  templateUrl: './editar-medicos.component.html',
  styleUrls: ['./editar-medicos.component.css']
})
export class EditarMedicosComponent implements OnInit{
  form!: FormGroup
  medicoVM!: FormsMedicoViewModel

  constructor(private formBuilder: FormBuilder, private medicosService: MedicosService, private toastrService: ToastrService, private router: Router, private route: ActivatedRoute){
  }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      nome: new FormControl('', [Validators.required]),
      crm: new FormControl('', [Validators.required])
    })

    this.route.data.pipe(map((dados) => dados['medico'])).subscribe({
      next: (medico) => this.obterMedico(medico),
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

    this.medicoVM = this.form.value

    const id = this.route.snapshot.paramMap.get('id')

    if(!id) return

    this.medicosService.editar(id, this.medicoVM).subscribe({
      next: () => this.processarSucesso(this.medicoVM),
      error: (erro) => this.processarFalha(erro)
    })
  }

  obterMedico(medico: FormsMedicoViewModel){
    this.medicoVM = medico
    this.form.patchValue(this.medicoVM)
  }

  processarSucesso(medico: FormsMedicoViewModel){
    this.toastrService.success(`O médico "${medico.nome}" foi editado com sucesso!`, 'Sucesso')
    this.router.navigate(['/medicos/listar'])
  }
  
  processarFalha(erro: Error){
    this.toastrService.error(erro.message, 'Error')
  }

}
