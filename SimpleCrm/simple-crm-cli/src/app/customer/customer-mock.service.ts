import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of} from 'rxjs';
import { Customer } from './customer.model';
import { CustomerService } from './customer.service';

@Injectable()
export class CustomerMockService extends CustomerService {
  customers: Customer[] = [];
  lastCustomerId: number;
  customer: Customer | undefined;

  constructor(http: HttpClient) {
    super(http);
    console.warn('Warning: You are using the CustomerMockService, not intended for production use.')

    const localCustomers = localStorage.getItem('customers');
  if (localCustomers) {
     this.customers = JSON.parse(localCustomers);
  } else {
    this.customers.push(
      {
        customerId: 1,
        firstName:'John',
        lastName: 'Smith',
        phoneNumber: '314-555-1234',
        emailAddress: 'john@nexualacademy.com',
        statusCode: 'Prospect',
        preferredContactMethod: 'phone',
        lastContactDate: new Date().toISOString()
      },
      {
        customerId: 2,
        firstName:'Tory',
        lastName: 'Amos',
        phoneNumber: '314-555-9873',
        emailAddress: 'tory@example.com',
        statusCode: 'Prospect',
        preferredContactMethod: 'email',
        lastContactDate: new Date().toISOString()
      }

    );
    localStorage.setItem('customers', JSON.stringify(this.customers));

  }
  this.lastCustomerId = Math.max(...this.customers.map(x => x.customerId));
 }
 override search(term: string): Observable<Customer[]> {

   return  of (this.customers.filter(c => c.emailAddress.startsWith(term) || c.lastName.startsWith(term)));
 }

 override insert(customer: Customer): Observable<Customer> {
  customer.lastContactDate = new Date()
    .toISOString();
  customer.customerId = Math.max(...this.customers.map(x => x.customerId))+ 1;
  this.customers = [...this.customers, customer];
  localStorage.setItem('customers', JSON.stringify(this.customers));
  return of (customer);
 }
 override update(customer: Customer): Observable<Customer> {

  customer.lastContactDate = new Date()
    .toISOString();
   let updateCustomers = this.customers.map(c => {
     if ( c.customerId == customer.customerId) {
      return customer;
     }
     else {
       return c;
     }
    })
      this.customers = updateCustomers;
      return of (customer);

      localStorage.setItem('customers', JSON.stringify(updateCustomers));
  };

  //override get(customerId:number): Observable<Customer>{
   // return this.http.get<Customer>(`/api/customer/${customerId}`);
   override get(customerId: number): Observable<Customer | undefined> {
    const item = this.customers.find(x => x.customerId === customerId) as Customer;
    return of(item);
  }
  }




