import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExcluirMedicosComponent } from './excluir-medicos.component';

describe('ExcluirMedicosComponent', () => {
  let component: ExcluirMedicosComponent;
  let fixture: ComponentFixture<ExcluirMedicosComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ExcluirMedicosComponent]
    });
    fixture = TestBed.createComponent(ExcluirMedicosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
