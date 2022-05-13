import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule} from '@angular/material/card';
import {MatTableModule} from '@angular/material/table';
import {HttpClientModule} from '@angular/common/http';


import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerListPageComponent } from './customer-list-page/customer-list-page.component';
import { MatButtonModule } from '@angular/material/button';
import { CustomerService } from './customer.service';


@NgModule({
  declarations: [
    CustomerListPageComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    CustomerRoutingModule,
    MatButtonModule,
    MatCardModule,
    MatTableModule,


  ],
  providers: [
    CustomerService
  ]
})
export class CustomerModule { }
