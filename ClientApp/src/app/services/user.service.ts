import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class UserService {
    private readonly apiUri = environment.apiUrl + '/api/users/';
    constructor(private http: HttpClient) { }

    getAgentUsers() {
        return this.http.get(this.apiUri + 'agents').pipe(
            map(res => res)
        );
    }

    getUserByEmail(id) {
        return this.http.get(this.apiUri + id).pipe(
            map(res => res)
        );
    }

    getUserByCampaign(campaignId) {
        return this.http.get(this.apiUri + 'campaign/' + campaignId).pipe(
            map(res => res)
        );
    }

}