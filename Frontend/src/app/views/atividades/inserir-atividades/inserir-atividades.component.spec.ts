import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InserirAtividadesComponent } from './inserir-atividades.component';

describe('InserirAtividadesComponent', () => {
  let component: InserirAtividadesComponent;
  let fixture: ComponentFixture<InserirAtividadesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InserirAtividadesComponent]
    });
    fixture = TestBed.createComponent(InserirAtividadesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
