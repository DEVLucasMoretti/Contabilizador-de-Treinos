import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalTreinoContabilizadoComSucesso } from './modal-treino-contabilizado-com-sucesso';

describe('ModalTreinoContabilizadoComSucesso', () => {
  let component: ModalTreinoContabilizadoComSucesso;
  let fixture: ComponentFixture<ModalTreinoContabilizadoComSucesso>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModalTreinoContabilizadoComSucesso]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalTreinoContabilizadoComSucesso);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
