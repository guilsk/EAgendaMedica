import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { LocalStorageService } from "src/app/core/auth/services/local-storage.service";
import { FormsAtividadeViewModel } from "../models/forms-atividade.view-model";
import { Observable, map } from "rxjs";
import { ListarAtividadeViewModel } from "../models/listar-atividade.view-model";
import { VisualizarAtividadeViewModel } from "../models/visualizar-atividade.view-model";

@Injectable({
    providedIn: 'root'
})
export class AtividadesService{
    private endpoint: string = ''

    constructor(private http: HttpClient, private localStorage: LocalStorageService) { }

    public inserir(atividade: FormsAtividadeViewModel): Observable<FormsAtividadeViewModel> {

        return this.http.post<any>(this.endpoint, atividade)
    }

    public editar(id: string, atividade: FormsAtividadeViewModel){
        return this.http.put<any>(this.endpoint + id, atividade).pipe(map((res) => res.dados))
    }

    public excluir(id: string): Observable<any>{
        return this.http.delete(this.endpoint + id)
    }

    public selecionarTodos(): Observable<ListarAtividadeViewModel[]>{
        return this.http.get<any>(this.endpoint).pipe(map((res) => res.dados))
    }
    
    public selecionarPorId(id: string): Observable<FormsAtividadeViewModel> {
        return this.http.get<any>(this.endpoint + id).pipe(map((res) => res.dados))
    }

    public selecionarAtividadeCompletoPorId(id: string): Observable<VisualizarAtividadeViewModel>{
        return this.http.get<any>(this.endpoint + 'visualizacao-completa/' + id).pipe(map((res) => res.dados))
    }
}