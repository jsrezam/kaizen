import { Product } from './../../models/product';
import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'src/app/services/customer.service';
import { EmployeeService } from 'src/app/services/employee.service';

@Component({
  selector: 'app-assign-customers-form',
  templateUrl: './assign-customers-form.component.html',
  styleUrls: ['./assign-customers-form.component.css']
})
export class AssignCustomersFormComponent implements OnInit {
  private readonly PAGE_SIZE = 3;
  columns = [
    { title: 'IsAssigned' },
    { title: 'Id' },
    { title: 'LastName', key: 'lastName', isSortable: true },
    { title: 'FirstName', key: 'firstName', isSortable: true },
    { title: 'Address', key: 'address', isSortable: true },
    { title: 'City', key: 'city', isSortable: true },
    { title: 'Region', key: 'region', isSortable: true },
    { title: 'HomePhone', key: 'homePhone', isSortable: true },
    { title: 'CellPhone', key: 'cellPhone', isSortable: true },
  ];

  // employees: any[];
  filteredCustomers: any[];
  queryResult: any = {
    items: []
  };

  query: any = {
    pageSize: this.PAGE_SIZE,
    page: 1
  };

  constructor(private customerService: CustomerService,
    private employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.populateCustomers();
    //this.populateEmployees();
  }
  // private populateEmployees() {
  //   this.employeeService.getEmployees(null)
  //     .subscribe((result: any) => {
  //       // this.employees = result
  //       console.log(result);
  //     });
  // }
  private populateCustomers() {
    this.customerService.getCustomers(null)
      .subscribe((result: any) => {
        this.queryResult = result;
        this.filteredCustomers = this.queryResult.items;
      });
  }

  onPageChage(page) {
    // console.log(page);
    this.query.page = page;
    //this.populateCustomers();
  }

  filter(querySearch: string) {
    console.log(querySearch);
    // console.log(this.customers)
    this.filteredCustomers = (querySearch) ?
      this.queryResult.items.filter(c => c.lastName.toLowerCase().includes(querySearch.toLowerCase())) :
      this.queryResult.items;

  }

}
