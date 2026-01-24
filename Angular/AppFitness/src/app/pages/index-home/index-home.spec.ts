import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndexHome } from './index-home';

describe('IndexHome', () => {
  let component: IndexHome;
  let fixture: ComponentFixture<IndexHome>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IndexHome]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IndexHome);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
