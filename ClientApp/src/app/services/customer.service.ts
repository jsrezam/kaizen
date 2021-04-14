import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class CustomerService {
    private readonly customersEndpoint = '/api/customers/';

    constructor(private http: HttpClient) { }

    getCustomers(filter) {
        return this.http.get(this.customersEndpoint + '?' + this.toQueryString(filter)).pipe(
            map(res => res)
        );
    }

    toQueryString(obj) {
        let parts = [];
        for (let property in obj) {
            let value = obj[property];
            if (value != null && value != undefined)
                parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
        }
        return parts.join('&');
    }

}