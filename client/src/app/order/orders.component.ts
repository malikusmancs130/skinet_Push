import { Component, OnInit } from '@angular/core';
import { IOrder } from '../shared/models/order';
import { OrderService } from './order.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {
  orders: IOrder[];

  constructor(private orderService: OrderService){

  }

  ngOnInit() {
    this.getOrders();
  }

  getOrders(){
    this.orderService.getOrdersForUser().subscribe((orders:IOrder[]) =>{
      this.orders = orders;
    },
    error =>{
      console.log(error);
    });
  }
}
