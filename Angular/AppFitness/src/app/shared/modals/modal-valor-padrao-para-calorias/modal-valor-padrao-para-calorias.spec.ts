import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalValorPadraoParaCalorias } from './modal-valor-padrao-para-calorias';

describe('ModalValorPadraoParaCalorias', () => {
  let component: ModalValorPadraoParaCalorias;
  let fixture: ComponentFixture<ModalValorPadraoParaCalorias>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModalValorPadraoParaCalorias]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalValorPadraoParaCalorias);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
