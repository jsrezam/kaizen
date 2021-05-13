import { CampaignService } from './../../services/campaign.service';
import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'src/app/services/customer.service';
import { UserService } from 'src/app/services/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-campaign-form',
  templateUrl: './campaign-form.component.html',
  styleUrls: ['./campaign-form.component.css']
})
export class CampaignFormComponent implements OnInit {
  private readonly PAGE_SIZE = 3;

  errorMessage: string;
  agentUsers: any[] = [];
  campaignSave: any = {};
  model: any = {};
  filteredCustomers: any[];
  queryResult: any = {
    items: []
  };
  query: any = {
    pageSize: this.PAGE_SIZE,
    page: 1
  };
  selectedfilterColumn: string;


  columns = [
    { title: '' },
    { title: 'LastName', key: 'lastName', isSortable: true },
    { title: 'FirstName', key: 'firstName', isSortable: false },
    { title: 'Address', key: 'address', isSortable: false },
    { title: 'City', key: 'city', isSortable: false },
    { title: 'Region', key: 'region', isSortable: false },
    { title: 'HomePhone', key: 'homePhone', isSortable: false },
    { title: 'CellPhone', key: 'cellPhone', isSortable: false },
  ];

  constructor(
    private router: Router,
    private toastrService: ToastrService,
    private userService: UserService,
    private campaignService: CampaignService,
    private customerService: CustomerService) { }

  ngOnInit(): void {
    this.populateCustomers();
    this.userService.getAgentUsers()
      .subscribe((result: any) => {
        this.agentUsers = result;
      });
  }

  private populateCustomers() {
    this.customerService.getCustomers(null)
      .subscribe((result: any) => {
        this.queryResult = result;
        this.filteredCustomers = this.queryResult.items;
      });
  }

  onPageChage(page) {
    this.query.page = page;
  }

  filter(querySearch: string) {

    switch (this.selectedfilterColumn) {
      case "lastName":
        this.filteredCustomers = (querySearch) ?
          this.queryResult.items.filter(c => c.lastName.toLowerCase().includes(querySearch.toLowerCase())) :
          this.queryResult.items;
        break;
    }

  }

  sortBy(columnName) {

    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }

    if (this.query.isSortAscending) {
      switch (columnName) {
        case "lastName":
          this.filteredCustomers.sort(function (a, b) {
            if (a.lastName < b.lastName) return -1;
            if (a.lastName > b.lastName) return 1;
            return 0;
          });
          break;
      }
    } else {
      switch (columnName) {
        case "lastName":
          this.filteredCustomers.sort(function (a, b) {
            if (b.lastName < a.lastName) return -1;
            if (b.lastName > a.lastName) return 1;
            return 0;
          });
          break;
      }
    }
  }

  create() {
    if (this.validateForm()) {
      this.campaignSave.startDate = new Date();
      this.campaignSave.finishDate = new Date(`${this.model.year}-${this.model.month}-${this.model.day}`);
      this.campaignSave.isActive = true;
      this.campaignSave.customers = this.filteredCustomers.filter(c => c.isSelected);

      this.campaignService.createCampaign(this.campaignSave)
        .subscribe((result: any) => {
          this.toastrService.success("Data was sucessfully saved.", "Success");
        });
      this.router.navigate(['/campaigns/']);
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
      this.errorMessage = "Please select at least one customer for new campaign"
      return false;
    } else if (!this.isValidDate()) {
      this.errorMessage = "The finish date of the new campaign must be greater than current date"
      return false;
    } else {
      return true;
    }
  }

}
