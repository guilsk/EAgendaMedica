import { TokenViewModel } from './../models/token.view-model';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  BehaviorSubject,
  Observable,
  catchError,
  map,
  tap,
  throwError,
} from 'rxjs';
import { RegistrarUsuarioViewModel } from '../models/registrar-usuario.view-model';
import { LocalStorageService } from './local-storage.service';
import { AutenticarUsuarioViewModel } from '../models/autenticar-usuario.view-model';
import { UsuarioTokenViewModel } from '../models/usuario-token.view-model';

@Injectable()
export class AuthService {
  private endpoint: string = 'https://e-agenda-web-api.onrender.com/api/conta/';

  private endpointRegistrar: string = this.endpoint + 'registrar';
  private endpointLogin: string = this.endpoint + 'autenticar';
  private endpointLogout: string = this.endpoint + 'sair';

  private usuarioAutenticado: BehaviorSubject<
    UsuarioTokenViewModel | undefined
  >;

  constructor(
    private http: HttpClient,
    private localStorage: LocalStorageService
  ) {
    this.usuarioAutenticado = new BehaviorSubject<
      UsuarioTokenViewModel | undefined
    >(undefined);
  }

  public obterUsuarioAutenticado() {
    return this.usuarioAutenticado.asObservable();
  }

  public registrar(
    usuario: RegistrarUsuarioViewModel
  ): Observable<TokenViewModel> {
    return this.http.post<any>(this.endpointRegistrar, usuario).pipe(
      // Mapeio a resposta completa para retornar apenas os dados
      map((res) => res.dados),

      // Obter o retorno do map e salvar no local-storage
      tap((dados: TokenViewModel) =>
        this.localStorage.salvarDadosLocaisUsuario(dados)
      ),
      catchError((err) => this.processarErroHttp(err))
    );
  }

  public login(
    usuario: AutenticarUsuarioViewModel
  ): Observable<TokenViewModel> {
    return this.http.post<any>(this.endpointLogin, usuario).pipe(
      // Mapeio a resposta completa para retornar apenas os dados
      map((res) => res.dados),

      // Obter o retorno do map e salvar no local-storage
      tap((dados: TokenViewModel) =>
        this.localStorage.salvarDadosLocaisUsuario(dados)
      ),

      // Notifica os componentes que o usuário efetuou login
      tap((dados: TokenViewModel) => this.notificarLogin(dados.usuarioToken)),

      catchError((err) => this.processarErroHttp(err))
    );
  }

  public logout(): Observable<any> {
    return this.http
      .post<any>(this.endpointLogout, {}, this.obterHeadersAutorizacao())
      .pipe(
        tap(() => this.notificarLogout()),
        tap(() => this.localStorage.limparDadosLocais())
      );
  }

  public logarUsuarioSalvo(): void {
    const dados = this.localStorage.obterDadosLocaisSalvos();

    if (!dados) return;

    // Data de expiração do Token
    const tokenEstaValido: boolean = new Date(dados.dataExpiracao) > new Date();

    // Notificar o login
    if (tokenEstaValido) this.notificarLogin(dados.usuarioToken);
  }

  private notificarLogin(usuario: UsuarioTokenViewModel): void {
    this.usuarioAutenticado.next(usuario);
  }

  private notificarLogout(): void {
    this.usuarioAutenticado.next(undefined);
  }

  private processarErroHttp(erro: HttpErrorResponse) {
    let mensagemErro = '';

    if (erro.status == 0)
      mensagemErro = 'Ocorreu um erro ao processar a requisição.';
    if (erro.status == 401)
      mensagemErro =
        'O usuário não está autorizado. Efetue login e tente novamente.';
    else mensagemErro = erro.error?.erros[0];

    return throwError(() => new Error(mensagemErro));
  }

  private obterHeadersAutorizacao() {
    const token = this.localStorage.obterDadosLocaisSalvos()?.chave;

    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      }),
    };
  }
}