import { Campaign } from './../models/campaign';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { toQueryString } from '../Utilities/Utilities';

@Injectable({ providedIn: 'root' })
export class CampaignService {
    private readonly apiUri = environment.apiUrl + '/api/campaigns/';

    constructor(private http: HttpClient) { }

    createCampaign(campaign) {
        return this.http.post(this.apiUri, campaign).pipe(
            map(res => res)
        );
    }

    getCampaign(id) {
        return this.http.get(this.apiUri + id).pipe(
            map(res => res)
        );
    }

    getUserCampaigns(filter, id) {
        return this.http.get(this.apiUri + "user/" + id + '?' + toQueryString(filter)).pipe(
            map(res => res)
        );
    }

    inactivateCampaign(campaign: Campaign) {
        return this.http.put(this.apiUri + campaign.id, campaign).pipe(
            map(res => res)
        );
    }
}