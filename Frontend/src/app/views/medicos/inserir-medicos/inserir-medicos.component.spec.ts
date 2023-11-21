import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InserirMedicosComponent } from './inserir-medicos.component';

describe('InserirMedicosComponent', () => {
  let component: InserirMedicosComponent;
  let fixture: ComponentFixture<InserirMedicosComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InserirMedicosComponent]
    });
    fixture = TestBed.createComponent(InserirMedicosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
