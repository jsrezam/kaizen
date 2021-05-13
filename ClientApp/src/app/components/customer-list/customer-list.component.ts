import { CustomerService } from './../../services/customer.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {
  private readonly PAGE_SIZE = 3;

  columns = [
    { title: 'First Name', key: 'firstName', isSortable: false },
    { title: 'Last Name', key: 'lastName', isSortable: false },
    { title: 'ID', key: 'identificationCard', isSortable: false },
    { title: 'Email', key: 'email', isSortable: false },
    { title: 'Cell Phone', key: 'cellPhone', isSortable: false },
    { title: 'Phone', key: 'homePhone', isSortable: false },
    { title: 'Country', key: 'country', isSortable: false },
    { title: 'Region', key: 'region', isSortable: false },
    { title: 'City', key: 'city', isSortable: false },
    { title: 'Zip', key: 'postalCode', isSortable: false },
    { title: 'Address', key: 'address', isSortable: false },
    { title: 'State', key: 'state', isSortable: false }
  ];

  customers: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };

  constructor(private customerService: CustomerService) { }

  ngOnInit(): void {
    this.populateCustomers();
  }

  populateCustomers() {
    this.customerService.getCustomers(this.query)
      .subscribe(response => {
        this.customers = response;
      })
  }

  onPageChage(page) {
    this.query.page = page;
    this.populateCustomers();
  }

}
