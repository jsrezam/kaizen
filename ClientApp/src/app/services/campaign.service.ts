import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { toQueryString } from '../common/common';

@Injectable({ providedIn: 'root' })
export class CampaignService {
    private readonly apiUri = environment.apiUrl + '/api/campaigns/';

    constructor(private http: HttpClient) { }

    createCampaign(campaign) {
        return this.http.post(this.apiUri, campaign).pipe(
            map(res => res)
        );
    }

    addCustomersToCampaign(customers, campaignId) {
        return this.http.post(this.apiUri + "addCustomers/" + campaignId, customers).pipe(
            map(res => res)
        );
    }

    getAgentCampaigns(filter, id) {
        return this.http.get(this.apiUri + "agents/" + id + '?' + toQueryString(filter)).pipe(
            map(res => res)
        );
    }

    getCampaign(campaignId) {
        return this.http.get(this.apiUri + campaignId).pipe(
            map(res => res)
        );
    }

    getAgentValidCampaigns(filter) {
        return this.http.get(this.apiUri + "agents/valids" + '?' + toQueryString(filter)).pipe(
            map(res => res)
        );
    }

    updateCampaign(campaign) {
        return this.http.put(this.apiUri + campaign.id, campaign).pipe(
            map(res => res)
        );
    }

    closeCampaign(campaign) {
        return this.http.post(this.apiUri + "close", campaign).pipe(
            map(res => res)
        );
    }

    openCampaign(campaign) {
        return this.http.post(this.apiUri + "open", campaign).pipe(
            map(res => res)
        );
    }
}