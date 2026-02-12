import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndexTreinos } from './index-treinos';

describe('IndexTreinos', () => {
  let component: IndexTreinos;
  let fixture: ComponentFixture<IndexTreinos>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IndexTreinos]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IndexTreinos);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
