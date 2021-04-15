import { Router } from '@angular/router';
import { AuthService } from './../../services/authService';
import { Component } from '@angular/core';

@Component({
  selector: 'logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent {
  constructor(private authService: AuthService, private router: Router) { }

  logOut() {
    this.authService.logOut();
    this.router.navigate(["/"]);
  }
}
