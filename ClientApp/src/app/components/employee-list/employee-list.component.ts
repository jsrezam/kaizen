import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/services/employee.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {
  private readonly PAGE_SIZE = 3;
  columns = [
    { title: 'Id' },
    { title: 'LastName', key: 'lastName', isSortable: true },
    { title: 'FirstName', key: 'firstName', isSortable: true },
    { title: 'Title', key: 'title', isSortable: true },
    { title: 'BirthDate', key: 'birthDate', isSortable: true },
    { title: 'HireDate', key: 'hireDate', isSortable: true },
    { title: 'Address', key: 'address', isSortable: true },
    { title: 'City', key: 'city', isSortable: true },
    { title: 'Region', key: 'region', isSortable: true },
    { title: 'HomePhone', key: 'homePhone', isSortable: true },
    { title: 'CellPhone', key: 'cellPhone', isSortable: true },
  ];

  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };

  constructor(private employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.populateEmployees();
  }

  private populateEmployees() {
    this.employeeService.getEmployees(null)
      .subscribe((result: any) => {
        this.queryResult = result
      });
  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateEmployees();
  }

  onPageChage(page) {
    this.query.page = page;
    this.populateEmployees();
  }

  delete(productId) {
    if (confirm("Are you sure?")) {
      this.employeeService.delete(productId)
        .subscribe(x => {
          this.populateEmployees();
        });
    }
  }

}
