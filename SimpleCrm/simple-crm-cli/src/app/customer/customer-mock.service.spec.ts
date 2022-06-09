import { TestBed } from '@angular/core/testing';
import { get } from 'http';
import { Observable, of } from 'rxjs';

import { CustomerMockService } from './customer-mock.service';

describe('CustomerMockService', () => {
  let service: CustomerMockService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CustomerMockService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});


