import { TestBed } from '@angular/core/testing';

import { TreinoApi } from './treino-api';

describe('TreinoApi', () => {
  let service: TreinoApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TreinoApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
