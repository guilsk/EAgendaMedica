import { Component, OnInit } from '@angular/core';
import { Observable, map } from 'rxjs';
import { AuthService } from '../auth/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit{
  estaColapsada: boolean = true

  usuarioEstaLogado$?: Observable<boolean>

  constructor(/*private authService: AuthService, */private router: Router) {}

  ngOnInit(): void {
    // this.usuarioEstaLogado$ = this.authService.obterUsuarioAutenticado().pipe(
    //   map((usuario) => {
    //     if (!usuario) return false;
    //     return true;
    //   })
    // );
  }

  sair(): void {/*
    this.authService.logout().subscribe(() => {
      this.router.navigate(['/login']);
    });*/
  }
}