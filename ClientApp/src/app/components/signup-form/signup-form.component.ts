import { Router } from '@angular/router';
import { AuthService } from './../../services/authService';
import { AuthenticationResponse } from './../../models/authenticationResponse';
import { UserCredentials } from './../../models/userCredentials';
import { Component } from '@angular/core';


@Component({
  selector: 'app-signup-form',
  templateUrl: './signup-form.component.html',
  styleUrls: ['./signup-form.component.css']
})
export class SignupFormComponent {

  userCredentials: UserCredentials = {}
  authenticationResponse: AuthenticationResponse = {};

  constructor(private authService: AuthService
    , private router: Router) { }

  signUp() {
    this.authService.signUp(this.userCredentials)
      .subscribe((response: any) => {
        this.router.navigate(['/login']);
      }, (err) => {
        this.authenticationResponse.errors = [...err.error]
      });
  }
}
