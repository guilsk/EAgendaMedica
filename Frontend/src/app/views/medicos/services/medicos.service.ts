import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { FormsMedicoViewModel } from "../models/forms-medico.view-model";
import { Observable, map } from "rxjs";
import { VisualizarMedicoViewModel } from "../models/visualizar-medico.view-model";
import { ListarMedicoViewModel } from "../models/listar-medico.view-model";

@Injectable({
    providedIn: 'root'
})
export class MedicosService {
    private endpoint: string = 'https://localhost:7214/api/medicos/'

    constructor(private http: HttpClient) { }

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
        return this.http.get<any>(this.endpoint + 'visualizacao-completa/' + id).pipe(map((res) => res.dados))
    }

    public selecionarMedicoCompletoPorId(id: string): Observable<VisualizarMedicoViewModel>{
        return this.http.get<any>(this.endpoint + 'visualizacao-completa/' + id).pipe(map((res) => res.dados))
    }

}