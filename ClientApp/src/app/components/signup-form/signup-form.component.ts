
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { AuthenticationResponse } from './../../models/authenticationResponse';
import { UserCredentials } from './../../models/userCredentials';
import { Component } from '@angular/core';
import { parseErrorsAPI } from 'src/app/Utilities/Utilities';


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

    if (this.userCredentials.password !== this.userCredentials.confirmPassword) {
      this.authenticationResponse.errors = [{ description: "The password must be match" }];
      return;
    }

    this.authService.signUp(this.userCredentials)
      .subscribe((response: any) => {
        this.router.navigate(['/login']);
      }, (err) => {
        this.authenticationResponse.errors = parseErrorsAPI(err);
      });
  }
}
