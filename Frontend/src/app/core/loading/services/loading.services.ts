import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable()
export class LoadingService {
  private estaCarregando = new BehaviorSubject<boolean>(false);

  obterStatusCarregamento(): Observable<boolean> {
    return this.estaCarregando.asObservable();
  }

  carregar() {
    this.estaCarregando.next(true);
  }

  parar() {
    this.estaCarregando.next(false);
  }
}