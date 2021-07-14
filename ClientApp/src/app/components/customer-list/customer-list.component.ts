import { CustomerService } from './../../services/customer.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html'
})
export class CustomerListComponent {
  private readonly PAGE_SIZE = 3;

  columns = [
    { title: 'First Name', key: 'firstName', isSortable: true, searchable: true },
    { title: 'Last Name', key: 'lastName', isSortable: true, searchable: true },
    { title: 'ID', key: 'identificationCard', isSortable: true, searchable: true },
    { title: 'Email', key: 'email', isSortable: true, searchable: true },
    { title: 'Cell Phone', key: 'cellPhone', isSortable: true, searchable: true, defaultSearch: true },
    { title: 'Phone', key: 'homePhone', isSortable: false, searchable: false },
    { title: 'Country', key: 'country', isSortable: false, searchable: false },
    { title: 'Region', key: 'region', isSortable: false, searchable: false },
    { title: 'City', key: 'city', isSortable: false, searchable: false },
    { title: 'Zip', key: 'postalCode', isSortable: false, searchable: false },
    { title: 'Address', key: 'address', isSortable: false, searchable: false },
    { title: 'State', key: 'state', isSortable: false, searchable: false }
  ];

  searchOption: any;
  searchPlaceholder: string;
  customers: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };

  constructor(private customerService: CustomerService) { }

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

  getDefaultColumnSearch() {
    return this.columns.find(c => c.defaultSearch);
  }

  setPlaceholderSearch() {
    if (!this.searchOption) {
      let defaultColumnSearch = this.getDefaultColumnSearch();
      this.searchPlaceholder = "Search by " + defaultColumnSearch.title;
      return this.searchPlaceholder;
    }
    let columnSearch = this.columns.find(c => c.key === this.searchOption);
    this.searchPlaceholder = "Search by " + columnSearch.title;
    return this.searchPlaceholder;
  }

  filterSearchOptions() {
    this.setPlaceholderSearch();
    return this.columns.filter(c => c.searchable);
  }

  resetFilter() {
    this.query = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };
  }

  onFilterChange() {
    this.setPlaceholderSearch();
    this.resetFilter();
  }

  search(querySearch) {

    this.resetFilter();

    if (!this.searchOption) {
      let defaultColumnSearch = this.getDefaultColumnSearch();
      this.query[defaultColumnSearch.key] = querySearch;
    } else {
      this.query[this.searchOption] = querySearch;
    }

    if (querySearch !== '')
      this.populateCustomers();
  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateCustomers();
  }

}
