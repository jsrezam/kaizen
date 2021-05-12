import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-customer-form',
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.css']
})
export class CustomerFormComponent implements OnInit {

  customer: any = {};
  isValidated: boolean;

  constructor() { }

  ngOnInit(): void {
  }

  createCustomer(form) {
    if (form.valid) {
      console.log("Hola");
    }
  }

}
