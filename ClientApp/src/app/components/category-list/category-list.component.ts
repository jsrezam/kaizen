import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {
  private readonly PAGE_SIZE = 3;

  columns = [
    { title: 'Name', key: 'name', isSortable: true, searchable: true, defaultSearch: true },
    { title: 'Description', key: 'description', isSortable: false, searchable: false },
  ];

  searchOption: any;
  searchPlaceholder: string;
  categories: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };

  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.populateCategories();
  }

  private populateCategories() {
    this.categoryService.getCategories(this.query)
      .subscribe((result: any) => {
        this.categories = result
      });
  }

  onPageChage(page) {
    this.query.page = page;
    this.populateCategories();
  }

  setPlaceholderSearch() {
    if (!this.searchOption) {
      var defaultColumnSearch = this.getDefaultColumnSearch();
      return this.searchPlaceholder = "Search by " + defaultColumnSearch.title;
    }
    var columnSearch = this.columns.find(c => c.key === this.searchOption);
    return this.searchPlaceholder = "Search by " + columnSearch.title;
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

    this.populateCategories();
  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateCategories();
  }

}
