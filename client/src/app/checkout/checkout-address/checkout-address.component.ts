import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/account/account.service';

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
    this.accountService.updateUserAddress(this.checkoutForm.get('addressForm').value).subscribe(()=>{
      this.toaster.success('Address Saved');
    },
    error => {
      this.toaster.error(error.message);
      console.log(error);
    });
  }

}
