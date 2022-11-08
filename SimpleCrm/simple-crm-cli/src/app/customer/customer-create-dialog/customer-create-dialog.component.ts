import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Customer } from '../customer.model';
import { CustomerService } from '../customer.service';
import { MatFormFieldControl } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';


@Component({
  selector: 'crm-customer-create-dialog',
  templateUrl: './customer-create-dialog.component.html',
  styleUrls: ['./customer-create-dialog.component.scss']
})
export class CustomerCreateDialogComponent implements OnInit {
  detailForm: FormGroup;
  customerId!: number;
  customer!: Customer | undefined;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<CustomerCreateDialogComponent>,
    private custService:CustomerService,
     @Inject(MAT_DIALOG_DATA) public data: Customer | null
     ) {
       this.detailForm = this.fb.group({
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        phoneNumber: [''],
        emailAddress: ['', [Validators.required, Validators.email]],
        preferredContactMethod: ['email']
       });

       if (this.data) { // ensure the object has a value first
        this.detailForm.patchValue(this.data); // the patchValue function updates the form input values.
       // data.firstName will be set into the form input named firstName, and so on.
     }
     }

  ngOnInit(): void {
  }

  save(): void {
    if (!this.detailForm.valid) {
      this.detailForm.markAllAsTouched();
      return;
    }

    let customerData = {
      ...this.data, // copies in all 'data' first
      ...this.detailForm.value // overwrites with any that exist from the form
    };

    this.custService.insert(customerData);
    this.dialogRef.close(customerData);
  }


  cancel(): void {
    this.dialogRef.close();
  }

}



