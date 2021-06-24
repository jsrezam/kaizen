import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { toQueryString } from '../common/common';

@Injectable({ providedIn: 'root' })
export class UserService {
    private readonly apiUri = environment.apiUrl + '/api/users/';
    constructor(private http: HttpClient) { }

    getAllUsers(filter) {
        return this.http.get(this.apiUri + '?' + toQueryString(filter)).pipe(
            map(res => res)
        );
    }

    getActiveAgents() {
        return this.http.get(this.apiUri + 'agents/actives').pipe(
            map(res => res)
        );
    }

    getAgentByCampaign(campaignId) {
        return this.http.get(this.apiUri + 'campaign/' + campaignId).pipe(
            map(res => res)
        );
    }

    changeUserState(user) {
        return this.http.post(this.apiUri + 'changeState', user).pipe(
            map(res => res)
        );
    }

}