import { getTestBed, TestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';

import { CustomerMockService } from './customer-mock.service';

describe('CustomerMockService', () => {
  let injector: TestBed;
  let service: CustomerMockService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CustomerMockService],
    });
    injector = getTestBed();
    service = injector.inject(CustomerMockService);
    httpMock = injector.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); // ensures there are no outstanding requests between tests.
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
  it('Is the customer Id that is expected returned', () => {
    service.get(1).subscribe((cust) => {
      if (cust) {
        expect(cust.customerId).toEqual(1);
        expect(cust.lastName).toEqual('Smith');
      }

    });
  });
});
