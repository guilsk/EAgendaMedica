import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { RouterModule } from '@angular/router';
import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';
import { LoadingModule } from './loading/loading.module';



@NgModule({
  declarations: [NavbarComponent],
  imports: [
    CommonModule,
    RouterModule,
    NgbCollapseModule,
    //AuthModule,
    LoadingModule
  ],
  exports: [NavbarComponent, /*AuthModule,*/ LoadingModule]
})
export class CoreModule { }
