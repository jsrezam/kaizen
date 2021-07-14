import { CampaignService } from './../../services/campaign.service';
import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'src/app/services/customer.service';
import { UserService } from 'src/app/services/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { parseErrorsAPI } from 'src/app/common/common';

@Component({
  selector: 'app-campaign-form',
  templateUrl: './campaign-form.component.html',
  styleUrls: ['./campaign-form.component.css']
})
export class CampaignFormComponent implements OnInit {
  private readonly PAGE_SIZE = 3;

  columns = [
    { title: '' },
    { title: 'First Name', key: 'firstName', isSortable: true, searchable: true },
    { title: 'Last Name', key: 'lastName', isSortable: true, searchable: true },
    { title: 'ID', key: 'identificationCard', isSortable: true, searchable: true },
    { title: 'Email', key: 'email', isSortable: true, searchable: true },
    { title: 'Cell Phone', key: 'cellPhone', isSortable: true, searchable: true, defaultSearch: true },
    { title: 'Phone', key: 'homePhone', isSortable: false, searchable: false },
    { title: 'Country', key: 'country', isSortable: true, searchable: true },
    { title: 'Region', key: 'region', isSortable: true, searchable: true },
    { title: 'City', key: 'city', isSortable: true, searchable: true },
    { title: 'Zip', key: 'postalCode', isSortable: false, searchable: false },
    { title: 'Address', key: 'address', isSortable: false, searchable: false },
    { title: 'State', key: 'state', isSortable: false, searchable: false }

  ];

  searchOption: any;
  searchPlaceholder: string;
  agents: any[] = [];
  agent: any = {};
  errorMessages: any[];
  campaignSave: any = {};
  model: any = {};
  filteredCustomers: any[];
  totalItems: number;
  queryResult: any = {
    items: []
  };
  query: any = {
    pageSize: this.PAGE_SIZE,
    page: 1
  };
  isSelectedAll: boolean;

  constructor(
    private router: Router,
    private toastrService: ToastrService,
    private userService: UserService,
    private campaignService: CampaignService,
    private customerService: CustomerService) { }

  ngOnInit(): void {
    this.populateActiveAgents();
  }

  private populateActiveAgents() {
    this.userService.getActiveAgents()
      .subscribe((result: any) => {
        this.agents = result;
      });
  }

  populateCustomers() {
    this.customerService.getAgentAvailableCustomers(this.agent)
      .subscribe((result: any) => {
        this.queryResult = result;
        this.filteredCustomers = this.queryResult.items;
        this.totalItems = this.queryResult.totalItems;
      });
  }

  onPageChage(page) {
    this.query.page = page;
  }

  setPlaceholderSearch() {
    if (!this.searchOption) {
      var defaultColumnSearch = this.getDefaultColumnSearch();
      this.searchPlaceholder = "Search by " + defaultColumnSearch.title;
      return this.searchPlaceholder;
    }
    var columnSearch = this.columns.find(c => c.key === this.searchOption);
    this.searchPlaceholder = "Search by " + columnSearch.title;
    return this.searchPlaceholder;
  }

  filterSearchOptions() {
    this.setPlaceholderSearch();
    return this.columns.filter(c => c.searchable);
  }

  getDefaultColumnSearch() {
    return this.columns.find(c => c.defaultSearch);
  }

  resetFilter() {
    this.query = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };
  }

  onCustomerFilterChange() {
    this.setPlaceholderSearch();
    this.resetFilter();
  }

  search(querySearch: string) {

    this.resetFilter();
    let columnSearch = this.searchOption;

    if (!this.searchOption) {
      let defaultColumnSearch = this.getDefaultColumnSearch();
      columnSearch = defaultColumnSearch.key;
    }

    let withoutNulls = this.queryResult.items.filter(c => c[columnSearch] != null)

    this.filteredCustomers = (querySearch) ? withoutNulls
      .filter(c => c[columnSearch]
        .toLowerCase()
        .includes(querySearch
          .toLowerCase())) : this.queryResult.items;

    this.totalItems = (querySearch) ? this.filteredCustomers.length :
      this.queryResult.items.length;
  }

  sortBy(columnName) {

    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }

    if (this.query.isSortAscending) {
      this.filteredCustomers.sort(function (a, b) {
        if (a[columnName] < b[columnName]) return -1;
        if (a[columnName] > b[columnName]) return 1;
        return 0;
      });
    } else {
      this.filteredCustomers.sort(function (a, b) {
        if (b[columnName] < a[columnName]) return -1;
        if (b[columnName] > a[columnName]) return 1;
        return 0;
      });
    }
  }

  anySelected() {
    let response = this.filteredCustomers.filter(c => c.isSelected);
    return response.length > 0
  }

  isValidDate() {
    let selectedDate = new Date(`${this.model.year}-${this.model.month}-${this.model.day}`);
    if (selectedDate <= new Date())
      return false
    return true;
  }

  validateForm() {
    if (!this.anySelected()) {
      this.errorMessages = parseErrorsAPI("Please select at least one customer for new campaign");
      return false;
    } else if (!this.isValidDate()) {
      this.errorMessages = parseErrorsAPI("The finish date of the new campaign must be greater than current date");
      return false;
    } else {
      return true;
    }
  }

  onAgentFilterChange() {
    this.agent = this.agents.find(a => a.id == this.campaignSave.userId);
    this.populateCustomers();
  }

  getRandomCampaign(maxRange) {
    this.resetFilter();

    if (!maxRange || !this.agent.id)
      return;

    this.query.id = this.agent.id;

    this.customerService.getRandomCustomers(maxRange, this.query)
      .subscribe((response: any) => {
        this.filteredCustomers = response.items;
        this.totalItems = response.totalItems;
        this.filteredCustomers.forEach(fc => fc["isSelected"] = true)
      });
  }

  SelectAll() {
    this.isSelectedAll = !this.isSelectedAll;
    this.filteredCustomers.forEach(fc => fc["isSelected"] = this.isSelectedAll);
  }

  create() {
    if (this.validateForm()) {
      this.campaignSave.startDate = new Date();
      this.campaignSave.finishDate = new Date(`${this.model.year}-${this.model.month}-${this.model.day}`);
      this.campaignSave.isActive = true;
      this.campaignSave.customers = this.filteredCustomers.filter(c => c.isSelected && c.state);

      this.campaignService.createCampaign(this.campaignSave)
        .subscribe((result: any) => {
          this.toastrService.success("Data was successfully saved.", "Success");
          this.router.navigate(['/campaigns/']);
        }, err => {
          this.errorMessages = parseErrorsAPI(err.error);
        });

    }
  }

}
