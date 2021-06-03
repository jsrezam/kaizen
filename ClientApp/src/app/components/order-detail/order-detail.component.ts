import { OrderService } from 'src/app/services/order.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.css']
})
export class OrderDetailComponent implements OnInit {

  order: any = {};
  orderDetail: any[] = [];
  customer: any = {};

  response: any = {};

  constructor(private route: ActivatedRoute,
    private router: Router,
    private orderService: OrderService) {

    this.route.params.subscribe(p => {
      this.order.id = +p['id'] || 0;
    });
  }

  ngOnInit(): void {
    this.orderService.getOrderDetailAsync(this.order.id)
      .subscribe((resp: any) => {
        this.order = resp;
        this.customer = this.order.customer;
        this.orderDetail = this.order.orderDetailViewModel;
      })
  }

  getTotalOrderPrice() {
    if (this.orderDetail.length === 0)
      return 0;
    return this.orderDetail.map(item => item.import).reduce((prev, next) => { return prev + next });
  }

}
