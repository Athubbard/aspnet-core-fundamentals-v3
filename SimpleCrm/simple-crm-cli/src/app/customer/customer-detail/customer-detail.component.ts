import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { CustomerCreateDialogComponent } from '../customer-create-dialog/customer-create-dialog.component';
import { Customer } from '../customer.model';
import { CustomerService } from '../customer.service';

@Component({
  selector: 'crm-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.scss']
})
export class CustomerDetailComponent implements OnInit {
  detailForm: FormGroup;
  customerId!: number;
  customer: Customer | undefined;



  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private customerService:CustomerService,
     ) {

       this.detailForm = this.fb.group({
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        phoneNumber: [''],
        emailAddress: ['', [Validators.required, Validators.email]],
        preferredContactMethod: ['email']
       });


     }




  ngOnInit(): void {
    this.customerId = +this.route.snapshot.params['id'];

    this.customerService.get(this.customerId)
    .subscribe(cust => {
      if (cust) {
        this.customer = cust;
      }
      this.customer = cust;
      //this.detailForm.patchValue(cust);
    })
  }

}
