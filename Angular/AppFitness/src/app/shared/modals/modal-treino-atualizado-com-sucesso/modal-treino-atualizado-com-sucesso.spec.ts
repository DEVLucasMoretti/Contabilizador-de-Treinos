import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalTreinoAtualizadoComSucesso } from './modal-treino-atualizado-com-sucesso';

describe('ModalTreinoAtualizadoComSucesso', () => {
  let component: ModalTreinoAtualizadoComSucesso;
  let fixture: ComponentFixture<ModalTreinoAtualizadoComSucesso>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModalTreinoAtualizadoComSucesso]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalTreinoAtualizadoComSucesso);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
