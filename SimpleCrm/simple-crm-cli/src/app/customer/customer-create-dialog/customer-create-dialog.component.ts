import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Customer } from '../customer.model';
import { CustomerService } from '../customer.service';
import { MatFormFieldControl } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from

@Component({
  selector: 'crm-customer-create-dialog',
  templateUrl: './customer-create-dialog.component.html',
  styleUrls: ['./customer-create-dialog.component.scss']
})
export class CustomerCreateDialogComponent implements OnInit {
  detailForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<CustomerCreateDialogComponent>,
    private custService:CustomerService,
     @Inject(MAT_DIALOG_DATA) public data: Customer | null
     ) {
       this.detailForm = this.fb.group({
        firstName: [''],
        lastName: [''],
        phoneNumber: [''],
        emailAddress: [''],
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
    const customer = {...this.data}
    //this.custService.insert(customer);
    this.dialogRef.close(customer);
  }

  cancel(): void {
    this.dialogRef.close();
  }

}

function MatImputModule(MatImputModule: any) {
  throw new Error('Function not implemented.');
}

