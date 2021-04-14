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

  employees: any[];
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };

  constructor(private customerService: CustomerService,
    private employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.populateCustomers();
    this.populateEmployees();
  }


  private populateEmployees() {
    this.employeeService.getEmployees(null)
      .subscribe((result: any) => {
        // this.employees = result
        console.log(result);
      });
  }
  private populateCustomers() {
    this.customerService.getCustomers(this.query)
      .subscribe((result: any) => {
        this.queryResult = result
      });
  }

  onPageChage(page) {
    this.query.page = page;
    this.populateCustomers();
  }

}
