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
import { MatSelectModule} from '@angular/material/select';
import {MatInputModule} from '@angular/material/input';
import { CustomerDetailComponent } from './customer-detail/customer-detail.component';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import { StatusIconPipe } from './status-icon.pipe';







@NgModule({
  declarations: [
    CustomerListPageComponent,
    CustomerCreateDialogComponent,
    CustomerDetailComponent,
    StatusIconPipe
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
    MatSelectModule,
    MatInputModule,
    MatSnackBarModule



  ],
  providers: [
    {
      provide: CustomerService,
      useClass: environment.production ? CustomerService : CustomerMockService
    }

  ]
})
export class CustomerModule { }
