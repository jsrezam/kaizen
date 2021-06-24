import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { toQueryString } from '../common/common';

@Injectable({ providedIn: 'root' })
export class OrderService {
    private readonly apiUri = environment.apiUrl + '/api/orders/';

    constructor(private http: HttpClient) { }

    createOrder(order) {
        return this.http.post(this.apiUri, order).pipe(
            map(res => res)
        );
    }

    getAgentOrdersByAgentId(agentId, filter) {
        return this.http.get(this.apiUri + agentId + '?' + toQueryString(filter)).pipe(
            map(res => res)
        );
    }

    getAgentOrders(filter) {
        return this.http.get(this.apiUri + 'agents' + '?' + toQueryString(filter)).pipe(
            map(res => res)
        );
    }

    getOrderDetailAsync(orderId) {
        console.log("Hola");
        return this.http.get(this.apiUri + 'orderDetail/' + orderId).pipe(
            map(res => res)
        );
    }

}