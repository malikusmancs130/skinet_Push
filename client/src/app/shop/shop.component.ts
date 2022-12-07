import { Component, OnInit } from '@angular/core';
import { IBrand } from '../shared/models/brands';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})

export class ShopComponent implements OnInit {
  ngOnInit() {
    this.getBrands();
    this.getTypes();
    this.getProducts();
  }

  brands: IBrand[];
  types: IType[];
  products: IProduct[];


  constructor(private shopService: ShopService)  {}

  getProducts() { 
    this.shopService.getProducts().subscribe(response => {
      this.products = response.data;
    }, error => {
      console.error();
    }
    )
  }

  getBrands() {
    this.shopService.getBrands().subscribe(response => {
      this.brands = response;
    }, error => {
      console.error();
    })
  }

  getTypes() {
    this.shopService.getTypes().subscribe(response => {
      this.types = response;
    }, error => {
      console.error();
    })
  }
}
