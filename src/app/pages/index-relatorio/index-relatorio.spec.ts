import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndexRelatorio } from './index-relatorio';

describe('IndexRelatorio', () => {
  let component: IndexRelatorio;
  let fixture: ComponentFixture<IndexRelatorio>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IndexRelatorio]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IndexRelatorio);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
