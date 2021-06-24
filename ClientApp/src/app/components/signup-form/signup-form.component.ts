
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { AuthenticationResponse } from './../../models/authenticationResponse';
import { UserCredentials } from './../../models/userCredentials';
import { Component } from '@angular/core';
import { parseErrorsAPI } from 'src/app/common/common';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-signup-form',
  templateUrl: './signup-form.component.html',
  styleUrls: ['./signup-form.component.css']
})
export class SignupFormComponent {

  userCredentials: UserCredentials = {}
  authenticationResponse: AuthenticationResponse = {};

  constructor(private authService: AuthService,
    private router: Router,
    private toastrService: ToastrService) { }

  signUp() {

    if (this.userCredentials.password !== this.userCredentials.confirmPassword) {
      this.authenticationResponse.errors = [{ description: "The password must be match" }];
      return;
    }

    this.userCredentials.email = this.userCredentials.userName;

    this.authService.signUp(this.userCredentials)
      .subscribe((response: any) => {
        this.toastrService.success("Now, you are signed up !", "Success");
        this.router.navigate(['/login']);
      }, (err) => {
        this.authenticationResponse.errors = parseErrorsAPI(err);
      });
  }
}
