import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndexContabilizarTreino } from './index-contabilizar-treino';

describe('IndexContabilizarTreino', () => {
  let component: IndexContabilizarTreino;
  let fixture: ComponentFixture<IndexContabilizarTreino>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IndexContabilizarTreino]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IndexContabilizarTreino);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
