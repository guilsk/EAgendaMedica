import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';
import { CoreModule } from './core/core.module';
import { DashboardModule } from './views/dashboard/dashboard.module';
import { MedicosModule } from './views/medicos/medicos.module';
import './extensions/form-group.extension'

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,

    NgbModule,
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-bottom-center',
      preventDuplicates: true,
    }),
    CoreModule,
    DashboardModule,
    MedicosModule,
    /*
    RegistroModule,
    LoginModule,
    */
  ],
  providers: [
    /*
    {
    provide: APP_INITIALIZER,
    useFactory: logarUsuarioSalvoFactory,
    deps: [AuthService],
    multi: true,
    }
    provideHttpClient(withInterceptors([httpTokenInterceptor])),
    */
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
