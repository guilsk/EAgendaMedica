import { inject } from '@angular/core';
import { CanActivateFn, UrlTree, Router } from '@angular/router';
import { Observable, map } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (): Observable<boolean | UrlTree> => {
  const router = inject(Router);

  return inject(AuthService)
    .obterUsuarioAutenticado()
    .pipe(
      map((usuario) => {
        // Nega acesso e redireciona o usuário
        if (!usuario) {
          return router.parseUrl('/login');
        }

        // Permite o usuário a acessar a rota
        return true;
      })
    );
};