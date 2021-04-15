import { Router } from '@angular/router';
import { AuthenticationResponse } from './../../models/authenticationResponse';
import { UserCredentials } from './../../models/userCredentials';
import { AuthService } from './../../services/authService';
import { Component } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  userCredentials: UserCredentials = {}
  authenticationResponse: AuthenticationResponse = {}

  constructor(private authService: AuthService
    , private router: Router) { }

  logIn() {
    this.authService.logIn(this.userCredentials)
      .subscribe((response: any) => {
        this.authService.saveToken(response)
        this.router.navigate(['/']);
      }, (err) => {
        this.authenticationResponse.errorMessage = err.error
      });
  }
}
