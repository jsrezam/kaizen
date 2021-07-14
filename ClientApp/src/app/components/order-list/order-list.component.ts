import { AuthService } from './../../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html'
})
export class OrderListComponent implements OnInit {
  private readonly PAGE_SIZE = 3;

  columns = [
    { title: '# Order', key: 'id', isSortable: false, searchable: true, defaultSearch: true },
    { title: '# Campaign', key: 'campaignDetail.campaignId', isSortable: true, searchable: true },
    { title: '# Detail', key: 'campaignDetailId', isSortable: true, searchable: true },
    { title: 'Customer Name', key: 'customerFirstName', isSortable: true, searchable: true },
    { title: 'Customer Last Name', key: 'customerLastName', isSortable: true, searchable: true },
    { title: 'Customer Cell Phone', key: 'customerCellPhone', isSortable: true, searchable: true },
    { title: 'Order Date', key: 'orderDate', isSortable: true, searchable: false },
  ];

  searchOption: any;
  searchPlaceholder: string;
  orders: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };

  agents: any[] = [];
  agent: any = {};

  constructor(public authService: AuthService,
    private userService: UserService,
    private orderService: OrderService) { }

  ngOnInit(): void {
    if (this.authService.isInRole('admin')) {
      this.populateActiveAgents();
    } else {
      this.populateOrders();
    }
  }

  private populateActiveAgents() {
    this.userService.getActiveAgents()
      .subscribe((result: any) => {
        this.agents = result;
      });
  }

  private populateOrders() {
    if (this.authService.isInRole('admin')) {
      this.orderService.getAgentOrdersByAgentId(this.agent.id, this.query)
        .subscribe((result: any) => {
          this.orders = result;
        });
    } else {
      this.orderService.getAgentOrders(this.query)
        .subscribe((result: any) => {
          this.orders = result;
        });
    }

  }

  onPageChage(page) {
    this.query.page = page;
    this.populateOrders();
  }

  getDefaultColumnSearch() {
    return this.columns.find(c => c.defaultSearch);
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
      var defaultColumnSearch = this.getDefaultColumnSearch();
      this.query[defaultColumnSearch.key] = querySearch;
    } else {
      this.query[this.searchOption] = querySearch;
    }

    this.populateOrders();

  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateOrders();
  }

  onAgentFilterChange() {
    this.resetFilter();
    this.populateOrders();
  }

}
