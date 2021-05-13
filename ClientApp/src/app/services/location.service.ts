import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class LocationService {
    private readonly apiUri = environment.apiUrl + '/api/locations/';

    constructor(private http: HttpClient) { }

    getCountries() {
        return this.http.get(this.apiUri + "countries").pipe(
            map(res => res)
        );
    }

    GetRegionsByCountry(countryId) {
        return this.http.get(this.apiUri + "regions/" + countryId).pipe(
            map(res => res)
        );
    }

    GetCitiesByRegion(regionId) {
        return this.http.get(this.apiUri + "cities/" + regionId).pipe(
            map(res => res)
        );
    }
}