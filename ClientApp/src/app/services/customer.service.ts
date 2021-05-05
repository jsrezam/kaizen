import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { toQueryString } from '../Utilities/Utilities';

@Injectable({ providedIn: 'root' })
export class CustomerService {
    private readonly apiUri = environment.apiUrl + '/api/customers/';

    constructor(private http: HttpClient) { }

    getCustomers(filter) {
        return this.http.get(this.apiUri + '?' + toQueryString(filter)).pipe(
            map(res => res)
        );
    }

    getUserCustomers(filter) {
        return this.http.get(this.apiUri + 'userCustomers/' + '?' + toQueryString(filter)).pipe(
            map(res => res)
        );
    }

}