import { AuthenticationResponse } from '../models/authenticationResponse';
import { environment } from '../../environments/environment';
import { UserCredentials } from '../models/userCredentials';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private readonly apiUri = environment.apiUrl + '/api/accounts/';
    private readonly tokenKey = 'token';
    private readonly expirationKey = 'token-expiration';
    private readonly roleKey = 'role';

    constructor(private http: HttpClient) { }

    signUp(credentials: UserCredentials) {
        return this.http.post(this.apiUri + 'signUp', credentials).pipe(
            map(res => res)
        );
    }

    logIn(userCredentials: UserCredentials) {
        return this.http.post(this.apiUri + 'login', userCredentials).pipe(
            map(res => res)
        )
    }

    logOut() {
        localStorage.removeItem(this.tokenKey);
        localStorage.removeItem(this.expirationKey);
    }

    saveToken(authenticationResponse: AuthenticationResponse) {
        localStorage.setItem(this.tokenKey, authenticationResponse.token);
        localStorage.setItem(this.expirationKey, authenticationResponse.expiration.toString());
    }

    isInRole(roleName: string): boolean {
        let roleNameClaim = this.getClaim(this.roleKey);
        if (!roleNameClaim) return false;
        if (roleNameClaim !== roleName) return false;
        return true;
    }

    getClaim(claim: string): string {
        if (!this.isAuthenticated())
            return;

        const token = localStorage.getItem(this.tokenKey);
        let dataToken = JSON.parse(atob(token.split('.')[1]));
        return dataToken[claim];
    }

    isAuthenticated() {
        let token = localStorage.getItem(this.tokenKey);
        let expiration = localStorage.getItem(this.expirationKey);
        let expirationDate = new Date(expiration);
        if (!token) return false;
        if (expirationDate <= new Date()) {
            this.logOut();
            return false;
        }

        return true;
    }

    getToken() {
        return localStorage.getItem(this.tokenKey);
    }

    makeAdmin(user) {
        return this.http.post(this.apiUri + 'makeAdmin', user).pipe(
            map(res => res)
        )
    }

    makeAgent(user) {
        return this.http.post(this.apiUri + 'makeAgent', user).pipe(
            map(res => res)
        )
    }
}