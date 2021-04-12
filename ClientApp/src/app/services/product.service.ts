import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment as env } from '../../environments/environment';
import { Product } from '../models/product';

@Injectable({ providedIn: 'root' })
export class ProductService {
    private readonly productsEndpoint = '/api/products/';

    constructor(private http: HttpClient) { }

    getProducts(filter) {
        return this.http.get(this.productsEndpoint + '?' + this.toQueryString(filter)).pipe(
            map(res => res)
        );
    }

    getProduct(id) {
        return this.http.get(this.productsEndpoint + id).pipe(
            map(res => res)
        );
    }

    create(product) {
        return this.http.post(env.dev.apiUrl + this.productsEndpoint, product).pipe(
            map(res => res)
        );
    }

    update(product: Product) {
        return this.http.put(env.dev.apiUrl + this.productsEndpoint + product.id, product).pipe(
            map(res => res)
        );
    }

    delete(id) {
        return this.http.delete(env.dev.apiUrl + this.productsEndpoint + id).pipe(
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