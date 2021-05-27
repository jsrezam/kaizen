import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { toQueryString } from '../Utilities/Utilities';

@Injectable({ providedIn: 'root' })
export class CustomerService {
    private readonly apiUri = environment.apiUrl + '/api/customers/';

    constructor(private http: HttpClient) { }

    createCustomer(customer) {
        return this.http.post(this.apiUri, customer).pipe(
            map(res => res)
        );
    }

    getCustomer(customerId) {
        return this.http.get(this.apiUri + customerId).pipe(
            map(res => res)
        );
    }

    getCustomers(filter) {
        return this.http.get(this.apiUri + '?' + toQueryString(filter)).pipe(
            map(res => res)
        );
    }

    getNoInCampaignCustomersAsync(filter, campaignId) {
        return this.http.get(this.apiUri + 'noInCampaign/' + campaignId + '?' + toQueryString(filter)).pipe(
            map(res => res)
        );
    }

    getAgentCustomers(filter) {
        return this.http.get(this.apiUri + 'agentCustomers/' + '?' + toQueryString(filter)).pipe(
            map(res => res)
        );
    }

    updateCustomer(customer) {
        return this.http.put(this.apiUri + customer.id, customer).pipe(
            map(res => res)
        );
    }

    getRandomCustomers(maxRange) {
        return this.http.get(this.apiUri + 'random/' + maxRange).pipe(
            map(res => res)
        );
    }

}