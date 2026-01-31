import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalConfirmUpdate } from './modal-confirm-update';

describe('ModalConfirmUpdate', () => {
  let component: ModalConfirmUpdate;
  let fixture: ComponentFixture<ModalConfirmUpdate>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModalConfirmUpdate]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalConfirmUpdate);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
