import { AuthService } from 'src/app/core/auth/services/auth.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { UsuarioTokenViewModel } from 'src/app/core/auth/models/usuario-token.view-model';
import { Subscription, Observable } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  // usuario$?: Observable<UsuarioTokenViewModel | undefined>;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    // this.usuario$ = this.authService.obterUsuarioAutenticado();
  }
}