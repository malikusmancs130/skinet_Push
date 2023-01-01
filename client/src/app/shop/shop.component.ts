import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from '../shared/models/brands';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';
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
    this.getProducts(true);
  }
  @ViewChild('search', {static:false}) searchTerm: ElementRef;
  brands: IBrand[];
  types: IType[];
  products: IProduct[];
  totalCount:number;
  shopParams : ShopParams;
  sortOptions= [
    {name: 'Alphabetical', value:'name'},
    {name:'Price: Low to High', value:'priceAsc'},
    {name:'Price: High to Low', value:'priceDesc'}
  ]


  constructor(private shopService: ShopService)  {
    this.shopParams=this.shopService.getShopParams();
  }

  getProducts(useCache = false) { 
    this.shopService.getProducts(useCache).subscribe(response => {
      this.products = response.data ;
      this.totalCount=response.count;
    }, error => {
      console.error();
    }
    )
  }

  getBrands() {
    this.shopService.getBrands().subscribe(response => {
      this.brands = [{id:0, name:'All'}, ... response]
    }, error => {
      console.error();
    })
  }

  getTypes() {
    this.shopService.getTypes().subscribe(response => {
      this.types = [{id:0, name:'All'}, ... response]
    }, error => {
      console.error();
    })
  }

  onBrandSelected(brandId: number){
    const params = this.shopService.getShopParams();
    params.brandId=brandId
    params.pageNumber=1;
    this.shopService.setShopParams(params);
    this.getProducts()
  }

  onTypeSelected(typeId: number){
    const params = this.shopService.getShopParams();
    params.typeId=typeId
    params.pageNumber=1;
    this.shopService.setShopParams(params);
    this.getProducts()
  }

  onSortSelected(event: Event)
  {
    const params = this.shopService.getShopParams();
    params.sort = (event.target as HTMLInputElement).value;
    this.shopService.setShopParams(params);
    this.getProducts();
  } 

  onPageChanged(event: any)
  {
    const params = this.shopService.getShopParams();
    if(params.pageNumber !== event)
    {
    params.pageNumber = event;
    this.shopService.setShopParams(params);
    this.getProducts(true);
    } 
   } 

  onSearch(){
    const params = this.shopService.getShopParams();
    params.search=this.searchTerm.nativeElement.value;
    params.pageNumber=1;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onReset(){
    this.searchTerm.nativeElement.value = '';
    this.shopParams= new ShopParams();
    this.shopService.setShopParams(this.shopParams);
    this.getProducts();
  }

}


