import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { LocalStorageService } from "src/app/core/auth/services/local-storage.service";
import { FormsMedicoViewModel } from "../models/forms-medico.view-model";
import { Observable, map } from "rxjs";
import { VisualizarMedicoViewModel } from "../models/visualizar-medico.view-model";
import { ListarMedicoViewModel } from "../models/listar-medico.view-model";

@Injectable({
    providedIn: 'root'
})
export class MedicosService {
    private endpoint: string = ''

    constructor(private http: HttpClient, private localStorage: LocalStorageService) { }

    public inserir(medico: FormsMedicoViewModel): Observable<FormsMedicoViewModel> {

        return this.http.post<any>(this.endpoint, medico)
    }

    public editar(id: string, medico: FormsMedicoViewModel){
        return this.http.put<any>(this.endpoint + id, medico).pipe(map((res) => res.dados))
    }

    public excluir(id: string): Observable<any>{
        return this.http.delete(this.endpoint + id)
    }

    public selecionarTodos(): Observable<ListarMedicoViewModel[]>{
        return this.http.get<any>(this.endpoint).pipe(map((res) => res.dados))
    }
    
    public selecionarPorId(id: string): Observable<FormsMedicoViewModel> {
        return this.http.get<any>(this.endpoint + id).pipe(map((res) => res.dados))
    }

    public selecionarMedicoCompletoPorId(id: string): Observable<VisualizarMedicoViewModel>{
        return this.http.get<any>(this.endpoint + 'visualizacao-completa/' + id).pipe(map((res) => res.dados))
    }

    // private processarErroHttp(erro: HttpErrorResponse) {
    //     let mensagemErro = '';

    //     if (erro.status == 0)
    //         mensagemErro = 'Ocorreu um erro ao processar a requisição.';
    //     if (erro.status == 401)
    //         mensagemErro =
    //             'O usuário não está autorizado. Efetue login e tente novamente.';
    //     else mensagemErro = erro.error?.erros[0];

    //     return throwError(() => new Error(mensagemErro));
    // }
    // private obterHeadersAutorizacao() {
    //     const token = this.localStorage.obterDadosLocaisSalvos()?.chave;

    //     return {
    //         headers: new HttpHeaders({
    //             'Content-Type': 'application/json',
    //             Authorization: `Bearer ${token}`,
    //         }),
    //     };
    // }
}