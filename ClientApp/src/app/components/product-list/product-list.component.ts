import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  private readonly PAGE_SIZE = 3;

  columns = [
    { title: 'Id' },
    { title: 'Category', key: 'category.name', isSortable: false },
    { title: 'Name', key: 'name', isSortable: true, searchable: true, defaultSearch: true },
    { title: 'Unit Price', key: 'unitPrice', isSortable: false },
    { title: 'Stock', key: 'unitsInStock', isSortable: false },
    { title: 'Units on Order', key: 'unitsOnOrder', isSortable: true },
    { title: 'State', key: 'discontinued', isSortable: false },
  ];

  searchOption: any;
  searchPlaceholder: string;
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.populateProducts();
  }

  private populateProducts() {
    this.productService.getProducts(this.query)
      .subscribe((result: any) => {
        this.queryResult = result
      });
  }

  onPageChage(page) {
    this.query.page = page;
    this.populateProducts();
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

    this.populateProducts();

  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateProducts();
  }

}
