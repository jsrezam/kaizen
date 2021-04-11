import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Category } from '../models/category';
import { environment as env } from '../../environments/environment';


@Injectable({ providedIn: 'root' })
export class CategoryService {
    private readonly categoriesEndpoint = '/api/categories/';

    constructor(private http: HttpClient) { }

    getCategory(id) {
        return this.http.get(this.categoriesEndpoint + id).pipe(
            map(res => res)
        );
    }

    getCategories(filter) {
        return this.http.get(this.categoriesEndpoint + '?' + this.toQueryString(filter)).pipe(
            map(res => res)
        );
    }

    create(category) {
        return this.http.post(env.dev.apiUrl + this.categoriesEndpoint, category).pipe(
            map(res => res)
        );
    }

    update(category: Category) {
        return this.http.put(env.dev.apiUrl + this.categoriesEndpoint + category.id, category).pipe(
            map(res => res)
        );
    }

    delete(id) {
        return this.http.delete(env.dev.apiUrl + this.categoriesEndpoint + id).pipe(
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