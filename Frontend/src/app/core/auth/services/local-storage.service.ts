import { Injectable } from '@angular/core';
import { TokenViewModel } from '../models/token.view-model';

@Injectable()
export class LocalStorageService {
  private chaveLocalStorage: string = 'e-agenda-dados';

  public salvarDadosLocaisUsuario(usuario: TokenViewModel) {
    const jsonString = JSON.stringify(usuario);

    localStorage.setItem(this.chaveLocalStorage, jsonString);
  }

  public obterDadosLocaisSalvos(): TokenViewModel | undefined {
    const jsonString = localStorage.getItem(this.chaveLocalStorage);

    if (!jsonString) return undefined;

    return JSON.parse(jsonString) as TokenViewModel;
  }

  public limparDadosLocais(): void {
    localStorage.setItem(this.chaveLocalStorage, '');
  }
}