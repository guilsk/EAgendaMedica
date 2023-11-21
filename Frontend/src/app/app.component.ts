import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Event, NavigationCancel, NavigationEnd, NavigationError, NavigationStart, Router } from '@angular/router';
import { LoadingService } from './core/loading/services/loading.services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  estaCarregando$?: Observable<boolean>

  constructor(private loadingService: LoadingService, private router: Router) {
    this.router.events.subscribe((event: Event) =>
      this.atualizarStatusCarregamento(event)
    )
  }

  ngOnInit(): void {
    this.estaCarregando$ = this.loadingService.obterStatusCarregamento();
  }

  atualizarStatusCarregamento(event: Event): void {
    if (event instanceof NavigationStart) {
      this.loadingService.carregar()
    } else if (event instanceof NavigationEnd ||
      event instanceof NavigationCancel ||
      event instanceof NavigationError
    ) {
      this.loadingService.parar();
    }
  }

}
