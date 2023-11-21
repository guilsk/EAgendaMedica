import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardMedicosComponent } from './card-medicos.component';

describe('CardMedicosComponent', () => {
  let component: CardMedicosComponent;
  let fixture: ComponentFixture<CardMedicosComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CardMedicosComponent]
    });
    fixture = TestBed.createComponent(CardMedicosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
