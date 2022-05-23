import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule} from '@angular/material/card';
import { MatTableModule} from '@angular/material/table';
import { HttpClientModule} from '@angular/common/http';


import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerListPageComponent } from './customer-list-page/customer-list-page.component';
import { MatButtonModule } from '@angular/material/button';
import { CustomerService } from './customer.service';
import { CustomerMockService } from './customer-mock.service';
import { environment } from 'src/environments/environment';
import { MatIconModule } from '@angular/material/icon';
import { FlexLayoutModule } from '@angular/flex-layout';
import { CustomerCreateDialogComponent } from './customer-create-dialog/customer-create-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { ReactiveFormsModule } from "@angular/forms";
import {MatFormFieldModule} from '@angular/material/form-field';






@NgModule({
  declarations: [
    CustomerListPageComponent,
    CustomerCreateDialogComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    CustomerRoutingModule,
    MatButtonModule,
    MatCardModule,
    MatTableModule,
    MatIconModule,
    FlexLayoutModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatIconModule,
    MatFormFieldModule
  ],
  providers: [
    {
      provide: CustomerService,
      useClass: environment.production ? CustomerService : CustomerMockService
    }

  ]
})
export class CustomerModule { }
