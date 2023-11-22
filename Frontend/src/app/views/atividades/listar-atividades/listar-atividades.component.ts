import { Component, OnInit } from '@angular/core';
import { ListarAtividadeViewModel } from '../models/listar-atividade.view-model';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs';

@Component({
  selector: 'app-listar-atividades',
  templateUrl: './listar-atividades.component.html',
  styleUrls: ['./listar-atividades.component.css']
})
export class ListarAtividadesComponent implements OnInit{
  atividades: ListarAtividadeViewModel[] = []

  constructor(private route: ActivatedRoute, private toastrService: ToastrService) {}

  ngOnInit(): void {
    this.route.data.pipe(map((dados) => dados['atividades'])).subscribe({
      next: (atividades) => this.obterAtividades(atividades),
      error: (erro) => this.processarFalha(erro)
    })
  }

  obterAtividades(atividades: ListarAtividadeViewModel[]){
    this.atividades = atividades
  }

  processarFalha(erro: Error){
    this.toastrService.error(erro.message, 'Erro')
  }
}
