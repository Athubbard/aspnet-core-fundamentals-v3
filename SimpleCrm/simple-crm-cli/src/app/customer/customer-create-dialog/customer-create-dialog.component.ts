import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Customer } from '../customer.model';
import { CustomerService } from '../customer.service';

@Component({
  selector: 'crm-customer-create-dialog',
  templateUrl: './customer-create-dialog.component.html',
  styleUrls: ['./customer-create-dialog.component.scss']
})
export class CustomerCreateDialogComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<CustomerCreateDialogComponent>,
    private custService:CustomerService,
     @Inject(MAT_DIALOG_DATA) public data: Customer | null ) { }

  ngOnInit(): void {
  }

  save(): void {
    const customer = {...this.data}
    //this.custService.insert(customer);
    this.dialogRef.close(customer);
  }

  cancel(): void {
    this.dialogRef.close();
  }

}

