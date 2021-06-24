import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { toQueryString } from '../common/common';

@Injectable({ providedIn: 'root' })
export class CampaignDetailService {
    private readonly apiUri = environment.apiUrl + '/api/campaignDetails/';

    constructor(private http: HttpClient) { }

    getCampaignDetail(filter, campaignId) {
        return this.http.get(this.apiUri + campaignId + '?' + toQueryString(filter)).pipe(
            map(res => res)
        );
    }

    removeDetailCampaignItem(campaignDetailItemId) {
        return this.http.delete(this.apiUri + campaignDetailItemId).pipe(
            map(res => res)
        );
    }
}