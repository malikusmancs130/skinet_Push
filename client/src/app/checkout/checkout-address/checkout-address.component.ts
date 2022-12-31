import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/account/account.service';
import { IAddress } from 'src/app/shared/models/address';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss']
})
export class CheckoutAddressComponent implements OnInit{

  @Input() checkoutForm: FormGroup;
 
  constructor(private accountService : AccountService, private toaster: ToastrService){

  }
 
  ngOnInit() {
    
  }

  saveUserAddress(){
    this.accountService.updateUserAddress(this.checkoutForm.get('addressForm').value)
    .subscribe((address: IAddress)=>{
      this.toaster.success('Address Saved');
      this.checkoutForm.get('addressForm').reset(address);
    },
    error => {
      this.toaster.error(error.message);
      console.log(error);
    });
  }

}