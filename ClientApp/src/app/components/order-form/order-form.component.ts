import { OrderService } from './../../services/order.service';
import { CategoryService } from './../../services/category.service';
import { ProductService } from 'src/app/services/product.service';
import { CustomerService } from './../../services/customer.service';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { parseErrorsAPI } from 'src/app/common/common';


@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styleUrls: ['./order-form.component.css']
})
export class OrderFormComponent implements OnInit {
  private readonly PAGE_SIZE = 3;

  customerColumns = [
    { title: 'First Name', key: 'firstName', isSortable: true, searchable: true },
    { title: 'Last Name', key: 'lastName', isSortable: true, searchable: true },
    { title: 'Cell Phone', key: 'cellPhone', isSortable: true, searchable: true, defaultSearch: true },
  ];

  productColumns = [
    { title: 'Product Id', key: 'id', isSortable: false, searchable: true },
    { title: 'Category', key: 'category.name', isSortable: false, searchable: true },
    { title: 'Name', key: 'name', isSortable: false, searchable: true, defaultSearch: true },
    { title: 'Unit Price', key: 'unitPrice', isSortable: false, searchable: false },
    { title: 'Stock', key: 'unitsInStock', isSortable: false },
    { title: 'Units on Order', key: 'unitsOnOrder', isSortable: true },
  ];

  //Customer variables
  searchCustomerOption: any;
  searchCustomerPlaceholder: string;
  customer: any;
  customers: any = {};
  customerQuery: any = {
    pageSize: this.PAGE_SIZE
  };
  isInCustomerPage: boolean = true;

  //Product variables
  searchProductOption: any;
  searchProductPlaceholder: string;
  products: any = {};
  productQuery: any = {
    pageSize: this.PAGE_SIZE
  };
  isInProductsPage: boolean = false;

  //Checkout variables
  isInCheckoutPage: boolean = false;
  cart: any[] = [];
  order: any = {};

  //Common variables
  errorMessages: any[];

  constructor(private customerService: CustomerService,
    private productService: ProductService,
    private orderService: OrderService,
    private toastrService: ToastrService) { }

  ngOnInit(): void { }

  //Customer configuration

  populateUserCustomers() {
    this.customerService.getAgentCustomers(this.customerQuery)
      .subscribe(response => {
        this.customers = response;
      })
  }

  onCustomerTablePageChage(page) {
    this.customerQuery.page = page;
    this.populateUserCustomers();
  }

  getCustomerDefaultColumnSearch() {
    return this.customerColumns.find(c => c.defaultSearch);
  }

  setCustomerPlaceholderSearch() {
    if (!this.searchCustomerOption) {
      let defaultColumnSearch = this.getCustomerDefaultColumnSearch();
      return this.searchCustomerPlaceholder = "Search by " + defaultColumnSearch.title;
    }
    let columnSearch = this.customerColumns.find(c => c.key === this.searchCustomerOption);
    return this.searchCustomerPlaceholder = "Search by " + columnSearch.title;
  }

  filterCustomerSearchOptions() {
    this.setCustomerPlaceholderSearch();
    return this.customerColumns.filter(c => c.searchable);
  }

  resetCustomerFilter() {
    this.customerQuery = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };
  }

  onCustomerFilterChange() {
    this.setCustomerPlaceholderSearch();
    this.resetCustomerFilter();
  }

  customerSearch(querySearch) {

    this.resetCustomerFilter();

    if (!this.searchCustomerOption) {
      let defaultColumnSearch = this.getCustomerDefaultColumnSearch();
      this.customerQuery[defaultColumnSearch.key] = querySearch;
    } else {
      this.customerQuery[this.searchCustomerOption] = querySearch;
    }

    if (querySearch !== '')
      this.populateUserCustomers();
  }

  sortCustomerBy(columnName) {
    if (this.customerQuery.sortBy === columnName) {
      this.customerQuery.isSortAscending = !this.customerQuery.isSortAscending;
    } else {
      this.customerQuery.sortBy = columnName;
      this.customerQuery.isSortAscending = true;
    }
    this.populateUserCustomers();
  }

  selectCustomer(selectedCustomer) {
    this.customer = selectedCustomer;
    this.customers.totalItems = 0;
  }

  //Product configuration

  populateProducts() {
    this.productService.getValidProducts(this.productQuery)
      .subscribe((result: any) => {
        this.products = result;
      });
  }

  onProductTablePageChage(page) {
    this.productQuery.page = page;
    this.populateProducts();
  }

  getProductDefaultColumnSearch() {
    return this.productColumns.find(c => c.defaultSearch);
  }

  setProductPlaceholderSearch() {
    if (!this.searchProductOption) {
      var defaultColumnSearch = this.getProductDefaultColumnSearch();
      return this.searchProductPlaceholder = "Search by " + defaultColumnSearch.title;
    }
    var columnSearch = this.productColumns.find(c => c.key === this.searchProductOption);
    return this.searchProductPlaceholder = "Search by " + columnSearch.title;
  }

  filterProductSearchOptions() {
    this.setProductPlaceholderSearch();
    return this.productColumns.filter(c => c.searchable);
  }

  resetProductFilter() {
    this.productQuery = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };
  }

  onProductFilterChange() {
    this.setProductPlaceholderSearch();
    this.resetProductFilter();
  }

  searchProduct(querySearch) {

    this.resetProductFilter();

    if (!this.searchProductOption) {
      var defaultColumnSearch = this.getProductDefaultColumnSearch();
      this.productQuery[defaultColumnSearch.key] = querySearch;
    } else {
      this.productQuery[this.searchProductOption] = querySearch;
    }

    this.populateProducts();

  }

  sortProductBy(columnName) {
    if (this.productQuery.sortBy === columnName) {
      this.productQuery.isSortAscending = !this.productQuery.isSortAscending;
    } else {
      this.productQuery.sortBy = columnName;
      this.productQuery.isSortAscending = true;
    }
    this.populateProducts();
  }

  //Navigation configuration

  goProductsPage() {
    this.isInCustomerPage = false;
    this.isInProductsPage = true;
    this.isInCheckoutPage = false;
  }

  goCustomerPage() {
    this.isInCustomerPage = true;
    this.isInProductsPage = false;
    this.isInCheckoutPage = false;
  }

  goCheckoutPage() {
    this.isInCustomerPage = false;
    this.isInProductsPage = false;
    this.isInCheckoutPage = true;
  }

  //Checkout configuration

  addToOrder(product) {
    let productInCart = this.cart.find(p => p.productId === product.id);

    if (!productInCart) {

      let newProduct = {
        productId: product.id,
        name: product.name,
        quantity: 1,
        unitsInStock: product.unitsInStock,
        unitPrice: product.unitPrice,
        totalPrice: 1 * product.unitPrice,
      };

      this.cart.push(newProduct);

    } else {
      if (productInCart.quantity === product.unitsInStock) {
        this.toastrService.info("Not enough products of this type available", "Info");
        return;
      }

      productInCart.quantity++;
      productInCart.totalPrice = productInCart.quantity * productInCart.unitPrice;
      let index = this.cart.indexOf(product);
      this.cart[index] = productInCart;
    }
  }

  reduceQuantityCartItem(product) {
    product.quantity--;
    product.totalPrice = product.quantity * product.unitPrice;
    if (product.quantity === 0) {
      let index = this.cart.indexOf(product);
      this.cart.splice(index, 1);
    }
  }

  increasedQuantityCartItem(product) {
    if (product.quantity === product.unitsInStock) {
      this.toastrService.info("Not enough products of this type available", "Info");
      return;
    }

    product.quantity++;
    product.totalPrice = product.quantity * product.unitPrice;
  }

  removeCartItem(product) {
    let index = this.cart.indexOf(product);
    this.cart.splice(index, 1);
  }

  getTotalOrderPrice() {
    if (this.cart.length === 0)
      return 0;
    return this.cart.map(item => item.totalPrice).reduce((prev, next) => { return prev + next });
  }

  creatOrder() {
    if (confirm("Are you sure?")) {
      this.order.campaignDetailId = this.customer.campaignDetailId;
      this.order.orderDetails = this.cart;
      this.orderService.createOrder(this.order)
        .subscribe(response => {
          this.toastrService.success("Data was successfully saved.", "Success");
        }, err => {
          this.errorMessages = parseErrorsAPI(err);
        })
    }
  }

}
