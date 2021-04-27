import { AuthService } from './auth.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';


@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(protected authService: AuthService, protected router: Router) { }

    canActivate() {
        if (!this.authService.isAuthenticated()) {
            this.router.navigate(['/login']);
            return false;
        }

        if (!this.authService.isInRole('admin')) {
            this.router.navigate(['/non-authorized']);
            return false;
        }

        return true;
    }
}