import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasket } from '../shared/models/basket';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit{

  basket$:Observable<IBasket>;

  constructor(private basketService : BasketComponent){

  }

  ngOnInit()  {
    this.basket$ = this.basketService.basket$;
  }

}
