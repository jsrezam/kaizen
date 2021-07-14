import { CampaignService } from 'src/app/services/campaign.service';
import { UserService } from './../../services/user.service';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { CampaignDetailService } from 'src/app/services/campaignDetail.service';
import { CustomerService } from 'src/app/services/customer.service';
import { formatDate, isExpiredDate, parseErrorsAPI } from 'src/app/common/common';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-campaign-detail',
  templateUrl: './campaign-detail.component.html',
  styleUrls: ['./campaign-detail.component.css']
})
export class CampaignDetailComponent implements OnInit {
  private readonly PAGE_SIZE = 3;


  customersColumns = [
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

  campaignDetailsColumns = [
    { title: 'Id' },
    { title: 'Customer First Name', key: 'customer.firstName', isSortable: true, searchable: true },
    { title: 'Customer Last Name', key: 'customer.lastName', isSortable: true, searchable: true },
    { title: 'Cell Phone', key: 'customer.cellPhone', isSortable: true, searchable: true, defaultSearch: true },
    { title: 'Total Calls Number', key: 'totalCallsNumber', isSortable: true, searchable: false },
    { title: 'Last Call Duration', key: 'lastCallDuration', isSortable: true, searchable: false },
    { title: 'Last Call Date', key: 'lastCallDate', isSortable: true, searchable: false },
    { title: 'Last Valid Call Duration', key: 'lastValidCallDuration', isSortable: true, searchable: false },
    { title: 'Last Valid Call Date', key: 'lastValidCallDate', isSortable: true, searchable: false },
    { title: 'Detail State', key: 'status', isSortable: false, searchable: false }
  ];

  customerSearchOption: any;
  customerSearchPlaceholder: string;
  customers: any = {};
  agent: any = {};
  campaign: any = {};
  campaignDetails: any = {};

  filteredCustomers: any[];
  totalItems: number;
  customerQuery: any = {
    pageSize: this.PAGE_SIZE,
    page: 1
  };
  isSelectedAllCustomers: boolean;

  searchOption: any;
  searchPlaceholder: string;
  query: any = {
    pageSize: this.PAGE_SIZE
  };
  errorMessages: any[];
  selectedCustomers: any[];


  constructor(private route: ActivatedRoute,
    private campaignService: CampaignService,
    private campaignDetailService: CampaignDetailService,
    private customerService: CustomerService,
    private userService: UserService,
    public authService: AuthService,
    private toastrService: ToastrService) {

    this.route.params.subscribe(c => {
      this.campaign.id = +c['id'] || 0;
    });

  }

  ngOnInit(): void {
    this.getCampaign();
    this.populateCustomers();
    this.getAgentByCampaign();
    this.populateCampaignDetails();
  }

  populateCustomers() {
    this.customerService.getNoInCampaignCustomersAsync({ ApplyPagingFromClient: true }, this.campaign.id)
      .subscribe((result: any) => {
        this.customers = result;
        this.filteredCustomers = [];
        this.filteredCustomers = this.customers.items;
        this.totalItems = this.customers.totalItems;
      });
  }

  onCustomerTablePageChage(page) {
    this.customerQuery.page = page;
  }

  setCustomersPlaceholderSearch() {
    if (!this.customerSearchOption) {
      var defaultColumnSearch = this.getDefaultCustomersColumnSearch();
      this.customerSearchPlaceholder = "Search by " + defaultColumnSearch.title;
      return this.customerSearchPlaceholder;
    }
    var columnSearch = this.customersColumns.find(c => c.key === this.customerSearchOption);
    this.customerSearchPlaceholder = "Search by " + columnSearch.title;
    return this.customerSearchPlaceholder;
  }

  filterCustomersSearchOptions() {
    this.setCustomersPlaceholderSearch();
    return this.customersColumns.filter(c => c.searchable);
  }

  getDefaultCustomersColumnSearch() {
    return this.customersColumns.find(c => c.defaultSearch);
  }

  resetCustomersFilter() {
    this.customerQuery = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };
  }

  onCustomerFilterChange() {
    this.setCustomersPlaceholderSearch();
    this.resetCustomersFilter();
  }

  searchCustomers(querySearch: string) {

    this.resetCustomersFilter();
    let columnSearch = this.customerSearchOption;

    if (!this.customerSearchOption) {
      let defaultColumnSearch = this.getDefaultCustomersColumnSearch();
      columnSearch = defaultColumnSearch.key;
    }

    let withoutNulls = this.customers.items.filter(c => c[columnSearch] != null)

    this.filteredCustomers = (querySearch) ? withoutNulls
      .filter(c => c[columnSearch]
        .toLowerCase()
        .includes(querySearch
          .toLowerCase())) : this.customers.items;

    this.totalItems = (querySearch) ? this.filteredCustomers.length :
      this.customers.items.length;
  }

  sortCustomerColumnsBy(columnName) {

    if (this.customerQuery.sortBy === columnName) {
      this.customerQuery.isSortAscending = !this.customerQuery.isSortAscending;
    } else {
      this.customerQuery.sortBy = columnName;
      this.customerQuery.isSortAscending = true;
    }

    if (this.customerQuery.isSortAscending) {
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

  SelectAllCustomers() {
    this.isSelectedAllCustomers = !this.isSelectedAllCustomers;
    this.filteredCustomers.forEach(fc => fc["isSelected"] = this.isSelectedAllCustomers);
  }

  onPageChage(page) {
    this.query.page = page;
    this.populateCampaignDetails();
  }

  setPlaceholderSearch() {
    if (!this.searchOption) {
      let defaultColumnSearch = this.getDefaultColumnSearch();
      this.searchPlaceholder = "Search by " + defaultColumnSearch.title;
      return this.searchPlaceholder;
    }
    let columnSearch = this.campaignDetailsColumns.find(c => c.key === this.searchOption);
    this.searchPlaceholder = "Search by " + columnSearch.title;
    return this.searchPlaceholder;
  }

  filterSearchOptions() {
    this.setPlaceholderSearch();
    return this.campaignDetailsColumns.filter(c => c.searchable);
  }

  getDefaultColumnSearch() {
    return this.campaignDetailsColumns.find(c => c.defaultSearch);
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

    this.populateCampaignDetails();
  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateCampaignDetails();
  }

  getAgentByCampaign() {
    this.userService.getAgentByCampaign(this.campaign.id)
      .subscribe((response: any) => {
        this.agent = response;
      });
  }

  getCampaign() {
    this.campaignService.getCampaign(this.campaign.id)
      .subscribe(response => {
        this.campaign = response;
      })
  }

  populateCampaignDetails() {
    this.campaignDetailService.getCampaignDetail(this.query, this.campaign.id)
      .subscribe((response: any) => {
        this.campaignDetails = response;
      });
  }

  updateFinishDate() {
    if (isExpiredDate(this.campaign.modelFinishDate)) {
      this.errorMessages = parseErrorsAPI("Finish date must be greater than current date.");
      return;
    }

    this.campaignService.updateCampaign(this.campaign)
      .subscribe(response => {
        this.campaign = response;
        this.errorMessages = [];
        this.toastrService.success("Finish date was successfully update.", "Success");
      });
  }

  addCustomersToCampaign() {

    if (this.isAvailableToAddCustomers()) {
      if (confirm(`Are you sure to add ${this.selectedCustomers.length} new customers?`)) {
        this.campaignService.addCustomersToCampaign(this.selectedCustomers, this.campaign.id)
          .subscribe(resp => {
            this.populateCustomers();
            this.populateCampaignDetails();
            this.errorMessages = [];
            this.toastrService.success("Customers added successfully.", "Success");
          }, err => {
            this.errorMessages = parseErrorsAPI(err);
          })
      }
    }

  }

  isAvailableToAddCustomers() {
    let response = true;
    this.selectedCustomers = this.filteredCustomers.filter(c => c.isSelected && c.state);
    let formateDate = formatDate(this.campaign.finishDate);

    if (isExpiredDate(formateDate)) {
      this.toastrService.info("Something missing in your request, please check the related message at the top of the form.", "Info");
      this.errorMessages = parseErrorsAPI("To add new customers you must first set a valid finish date for the campaign.");
      return !response;
    }

    if (this.selectedCustomers.length === 0) {
      this.toastrService.info("Something missing in your request, please check the related message at the top of the form.", "Info");
      this.errorMessages = parseErrorsAPI("You must select at least one customer to add in the campaign.");
      return !response;
    }

    return response;

  }

  remove(campaignDetailItemId) {
    if (confirm("Are you sure to remove this item?")) {
      this.campaignDetailService.removeDetailCampaignItem(campaignDetailItemId)
        .subscribe(response => {
          this.populateCustomers();
          this.populateCampaignDetails();
        });
    }
  }

  updateCampaignState() {
    if (confirm("Are you sure to close the campaign?")) {

      if (this.authService.isInRole('admin')) {
        let result$ = (this.campaign.isActive) ? this.campaignService.closeCampaign(this.campaign) : this.campaignService.openCampaign(this.campaign);

        result$.subscribe(response => {
          this.getCampaign();
          this.populateCampaignDetails();
          this.toastrService.success("Data was updated succesfully.", "Success")
          this.errorMessages = [];
        }, err => {
          this.errorMessages = parseErrorsAPI(err.error);
        });
      } else {
        if (this.isEnableToCloseCampaign()) {
          this.campaign.isActive = !this.campaign.isActive;
          this.campaignService.closeCampaign(this.campaign)
            .subscribe(response => {
              this.getCampaign();
              this.populateCampaignDetails();
              this.toastrService.success("Campaign was closed successfully.", "Success");
              this.errorMessages = [];
            });
        } else {
          this.errorMessages = parseErrorsAPI(`You can't close this campaign. 
          Please check that all items in the campaign are in 'Called' or 'Earned' state. 
          Make sure to sync your calls for the day in your Kaizen movil app.`);
        }
      }
    }
  }

  isEnableToCloseCampaign() {
    var campaignsOpen = this.campaignDetails.items.filter(cd => cd.state === 'Uncalled')
    return campaignsOpen.length === 0;
  }

  refreshDetail() {
    this.populateCampaignDetails();
  }

}