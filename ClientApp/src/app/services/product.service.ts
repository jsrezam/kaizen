import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Product } from '../models/product';

@Injectable({ providedIn: 'root' })
export class ProductService {
    private readonly apiUri = environment.apiUrl + '/api/products/';

    constructor(private http: HttpClient) { }

    getProducts(filter) {
        return this.http.get(this.apiUri + '?' + this.toQueryString(filter)).pipe(
            map(res => res)
        );
    }

    getProduct(id) {
        return this.http.get(this.apiUri + id).pipe(
            map(res => res)
        );
    }

    create(product) {
        return this.http.post(this.apiUri, product).pipe(
            map(res => res)
        );
    }

    update(product: Product) {
        return this.http.put(this.apiUri + product.id, product).pipe(
            map(res => res)
        );
    }

    delete(id) {
        return this.http.delete(this.apiUri + id).pipe(
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