import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardAtividadesComponent } from './card-atividades.component';

describe('CardAtividadesComponent', () => {
  let component: CardAtividadesComponent;
  let fixture: ComponentFixture<CardAtividadesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CardAtividadesComponent]
    });
    fixture = TestBed.createComponent(CardAtividadesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
