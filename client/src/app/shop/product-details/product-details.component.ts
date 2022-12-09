import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product:IProduct

  
  ngOnInit(){
    this.loadProductById();
  }
constructor(private shopService: ShopService, private activateRoute : ActivatedRoute , private bcService : BreadcrumbService){}

loadProductById(){
  this.shopService.getProduct(+this.activateRoute.snapshot.paramMap.get('id')).subscribe(product =>
    {
      this.product=product
      this.bcService.set('@productDetails', product.name);
    },error => {

      console.error();
});
}

}
