import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { toQueryString } from '../Utilities/Utilities';


@Injectable({ providedIn: 'root' })
export class CategoryService {
    private readonly apiUri = environment.apiUrl + '/api/categories/';

    constructor(private http: HttpClient) { }

    getCategories(filter) {
        return this.http.get(this.apiUri + '?' + toQueryString(filter)).pipe(
            map(res => res)
        );
    }

    getCategory(id) {
        return this.http.get(this.apiUri + id).pipe(
            map(res => res)
        );
    }

    create(category) {
        return this.http.post(this.apiUri, category).pipe(
            map(res => res)
        );
    }

    update(category) {
        return this.http.put(this.apiUri + category.id, category).pipe(
            map(res => res)
        );
    }

    delete(id) {
        return this.http.delete(this.apiUri + id).pipe(
            map(res => res)
        );
    }

}