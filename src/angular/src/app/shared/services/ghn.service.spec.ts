import { TestBed } from '@angular/core/testing';

import { GhnService } from './ghn.service';

describe('GhnService', () => {
  let service: GhnService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GhnService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
