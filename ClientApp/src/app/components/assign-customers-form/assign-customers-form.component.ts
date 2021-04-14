import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-assign-customers-form',
  templateUrl: './assign-customers-form.component.html',
  styleUrls: ['./assign-customers-form.component.css']
})
export class AssignCustomersFormComponent implements OnInit {
  private readonly PAGE_SIZE = 3;
  columns = [
    { title: '' },
    { title: 'Id' },
    { title: 'LastName', key: 'lastName', isSortable: true },
    { title: 'FirstName', key: 'firstName', isSortable: true },
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

  constructor() { }

  ngOnInit(): void {
  }

}
