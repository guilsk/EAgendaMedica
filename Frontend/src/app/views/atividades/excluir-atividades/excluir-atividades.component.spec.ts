import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExcluirAtividadesComponent } from './excluir-atividades.component';

describe('ExcluirAtividadesComponent', () => {
  let component: ExcluirAtividadesComponent;
  let fixture: ComponentFixture<ExcluirAtividadesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ExcluirAtividadesComponent]
    });
    fixture = TestBed.createComponent(ExcluirAtividadesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
