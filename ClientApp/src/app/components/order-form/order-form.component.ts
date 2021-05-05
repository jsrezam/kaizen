import { CategoryService } from './../../services/category.service';
import { ProductService } from 'src/app/services/product.service';
import { CustomerService } from './../../services/customer.service';
import { Customer } from './../../models/customer';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styleUrls: ['./order-form.component.css']
})
export class OrderFormComponent implements OnInit {
  private readonly PAGE_SIZE = 3;
  searchFieldCustomer: string;
  searchFieldProduct: string;
  // searchFieldCategory: string;
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };
  productQueryResult: any = {};
  productQuery: any = {
    pageSize: this.PAGE_SIZE
  };
  categoryQueryResult: any = {};
  customerColumns = [
    { title: 'First Name', key: 'firstName', isSortable: true },
    { title: 'Last Name', key: 'lastName', isSortable: true },
    { title: 'Cell Phone', key: 'cellPhone', isSortable: true }
  ];
  productColumns = [
    { title: 'Product Id' },
    { title: 'Category', key: 'category', isSortable: true },
    { title: 'Name', key: 'name', isSortable: true },
    { title: 'Unit Price', key: 'unitPrice', isSortable: true }
  ];

  customer: any;
  isInCustomerPage: boolean = true;
  constructor(private customerService: CustomerService,
    private categoryService: CategoryService,
    private productService: ProductService
  ) { }

  ngOnInit(): void {

  }

  private populateCategories() {
    this.categoryService.getCategories(null)
      .subscribe((result: any) => {
        this.categoryQueryResult = result;
      });
  }

  private populateProduct() {
    this.productService.getProduct(this.productQuery)
      .subscribe((result: any) => {
        this.productQuery = result;
      });
  }
  private populateUserCustomers() {
    this.customerService.getUserCustomers(this.query)
      .subscribe((result: any) => {
        this.queryResult = result;
      });
  }

  customerSearch(querySearch) {
    this.resetCustomerFilter();

    if (querySearch === "")
      return;

    switch (this.searchFieldCustomer) {
      case "firstName":
        this.query.firstname = querySearch
        break;
      case "lastName":
        this.query.lastName = querySearch
        break;
      case "cellPhone":
        this.query.cellPhone = querySearch
        break;
    }
    this.populateUserCustomers()
  }

  productSearch(productQuerySearch) {

    if (productQuerySearch === "")
      return;

    if (this.productQuery.categoryId) {

    }


    // switch (this.searchFieldCustomer) {
    //   case "category":
    //     this.productQuery.category = productQuerySearch
    //     break;
    //   case "name":
    //     this.productQuery.lastName = productQuerySearch
    //     break;
    //   case "unitPrice":
    //     this.productQuery.cellPhone = productQuerySearch
    //     break;
    // }
    //this.populateProduct();
  }

  resetProductFilter() {
    this.productQuery = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };
    // this.customer = null;
  }
  resetCustomerFilter() {
    this.query = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };
    this.customer = null;
  }

  selectCustomer(selectedCustomer) {
    this.customer = selectedCustomer;
    this.queryResult.totalItems = 0;
  }

  onPageChage(page) {
    this.query.page = page;
    this.populateUserCustomers();
  }

  onFilterChange() {
    if (this.searchFieldProduct !== "category")
      this.productQuery.categoryId = null;
    this.populateCategories();
  }

  NextPage() {
    this.isInCustomerPage = false;
  }

  PreviousPage() {
    this.isInCustomerPage = true;
  }


}
