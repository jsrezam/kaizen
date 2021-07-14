import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html'
})
export class UserListComponent implements OnInit {
  private readonly PAGE_SIZE = 10;

  columns = [
    { title: 'First Name', key: 'firstName', isSortable: true, searchable: true },
    { title: 'Last Name', key: 'lastName', isSortable: true, searchable: true },
    { title: 'ID', key: 'identificationCard', isSortable: true, searchable: true },
    { title: 'User Name', key: 'userName', isSortable: true, searchable: true, defaultSearch: true }
  ];

  searchOption: any;
  searchPlaceholder: string;
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };

  constructor(private userService: UserService,
    private authService: AuthService,
    private toastrService: ToastrService) { }

  ngOnInit(): void {
    this.populateUsers();
  }

  private populateUsers() {
    this.userService.getAllUsers(this.query)
      .subscribe((result: any) => {
        this.queryResult = result
      });
  }

  onPageChage(page) {
    this.query.page = page;
    this.populateUsers();
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

    this.populateUsers();

  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateUsers();
  }

  chageUserState(user) {
    this.userService.changeUserState(user)
      .subscribe((reponse: any) => {
        this.toastrService.success("Data was successfully saved.", "Success");
        user.isActive = reponse.isActive;
      });
  }

  changeUserRole(user) {
    if (user.role === 'agent') {
      this.authService.makeAdmin(user)
        .subscribe((resp: any) => {
          this.toastrService.success("Data was successfully saved.", "Success");
          user.role = resp.role;
        });
    } else {
      this.authService.makeAgent(user)
        .subscribe((resp: any) => {
          this.toastrService.success("Data was successfully saved.", "Success");
          user.role = resp.role;
        });
    }
  }

}
