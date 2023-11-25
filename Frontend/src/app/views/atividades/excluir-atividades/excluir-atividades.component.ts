import { Component, Inject, LOCALE_ID } from '@angular/core';
import { AtividadesService } from '../services/atividades.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs';
import { VisualizarAtividadeViewModel } from '../models/visualizar-atividade.view-model';
import {formatDate} from '@angular/common';

@Component({
  selector: 'app-excluir-atividades',
  templateUrl: './excluir-atividades.component.html',
  styleUrls: ['./excluir-atividades.component.css']
})
export class ExcluirAtividadesComponent {
  atividadeVM!: VisualizarAtividadeViewModel

  constructor(private atividadesService: AtividadesService, 
    private toastrService: ToastrService, 
    private route: ActivatedRoute, 
    private router: Router, 
    @Inject(LOCALE_ID) private locale: string){}

  ngOnInit(): void {
    this.route.data.pipe(map((dados) => dados['atividade'])).subscribe({
      next: (atividade) => this.obterAtividade(atividade),
      error: (erro) => this.processarFalha(erro)
    })
  }

  gravar(){
    this.atividadesService.excluir(this.atividadeVM.id).subscribe({
      next: () => this.processarSucesso(),
      error: (erro) => this.processarFalha(erro)
    })
  }

  obterAtividade(atividade: VisualizarAtividadeViewModel){
    this.atividadeVM = atividade
    this.atividadeVM.data = formatDate(atividade.data, 'dd/MM/yyyy', this.locale)
  }

  processarSucesso(){
    this.toastrService.success(`A atividade foi excluída com sucesso!`, 'Sucesso')
    this.router.navigate(['/atividades', 'listar'])
  }

  processarFalha(erro: Error){
    this.toastrService.error(erro.message, 'Error')
  }

}
