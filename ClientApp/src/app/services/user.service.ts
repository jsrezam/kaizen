import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class UserService {
    private readonly apiUri = environment.apiUrl + '/api/users/';
    constructor(private http: HttpClient) { }

    getActiveAgents() {
        return this.http.get(this.apiUri + 'agents/actives').pipe(
            map(res => res)
        );
    }

    getUserByEmail(id) {
        return this.http.get(this.apiUri + id).pipe(
            map(res => res)
        );
    }

    getAgentByCampaign(campaignId) {
        return this.http.get(this.apiUri + 'campaign/' + campaignId).pipe(
            map(res => res)
        );
    }

}