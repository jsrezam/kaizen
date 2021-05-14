import { CategoryService } from './../../services/category.service';
import { ProductService } from 'src/app/services/product.service';
import { CustomerService } from './../../services/customer.service';
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
    { title: 'First Name', key: 'firstName', isSortable: false },
    { title: 'Last Name', key: 'lastName', isSortable: false },
    { title: 'Cell Phone', key: 'cellPhone', isSortable: false }
  ];
  productColumns = [
    { title: 'Product Id', key: 'id', isSortable: false, searchable: true },
    { title: 'Category', key: 'category', isSortable: false, searchable: true },
    { title: 'Name', key: 'name', isSortable: false, searchable: true },
    { title: 'Unit Price', key: 'unitPrice', isSortable: false, searchable: false }
  ];

  customer: any;

  isInCustomerPage: boolean = true;
  isInProductsPage: boolean = false;
  isInCheckoutPage: boolean = false;

  cart: any[] = [];
  order: any = {};

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
    this.productService.getValidProducts(this.productQuery)
      .subscribe((result: any) => {
        this.productQueryResult = result;
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

    if (this.productQuery.categoryId) {
      if (productQuerySearch !== "") {
        this.productQuery.name = productQuerySearch;
      }
    }

    switch (this.searchFieldProduct) {
      case "id":
        this.productQuery.id = productQuerySearch
        break;
      case "name":
        this.productQuery.name = productQuerySearch
        break;
    }

    this.populateProduct();
    this.resetProductFilter();
  }

  resetProductFilter() {
    this.productQuery = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };
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

  onPageChageProduct(page) {
    this.productQuery.page = page;
    this.populateProduct();
  }

  onFilterChange() {
    this.resetProductFilter();
    this.populateCategories();
  }

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

  filterProductColumns() {
    return this.productColumns.filter(pc => pc.searchable);
  }

  addToOrder(product) {
    let productInCart = this.cart.find(p => p.productId === product.id);

    if (!productInCart) {

      let newProduct = {
        productId: product.id,
        name: product.name,
        quantity: 1,
        unitPrice: product.unitPrice,
        totalPrice: 1 * product.unitPrice,
      };

      this.cart.push(newProduct);

    } else {
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
      this.order.orderDate = new Date();
      this.order.requiredDate = new Date();
      this.order.shippedDate = new Date();
      this.order.orderDetails = this.cart;
      console.log(this.order);

    }
  }

}
