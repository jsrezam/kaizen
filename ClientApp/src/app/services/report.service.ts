import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class ReportService {
    private readonly apiUri = environment.apiUrl + '/api/reports/';

    constructor(private http: HttpClient) { }

    getTotalSalesByMonth() {
        return this.http.get(this.apiUri + 'totalSalesByMonth').pipe(
            map(res => res)
        );
    }

    getTotalSalesByAgent() {
        return this.http.get(this.apiUri + 'totalSalesByAgent').pipe(
            map(res => res)
        );
    }

    getTopCustomers() {
        return this.http.get(this.apiUri + 'topCustomers').pipe(
            map(res => res)
        );
    }

    getTopSellingProducts() {
        return this.http.get(this.apiUri + 'topSellingProducts').pipe(
            map(res => res)
        );
    }

    getTopAgent() {
        return this.http.get(this.apiUri + 'topAgent').pipe(
            map(res => res)
        );
    }

}