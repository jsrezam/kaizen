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
    { title: 'Category', key: 'category.name', isSortable: true },
    { title: 'Name', key: 'name', isSortable: true },
    { title: 'Unit Price', key: 'unitPrice', isSortable: true },
    { title: 'Stock', key: 'unitsInStock', isSortable: true },
    { title: 'Units on order', key: 'unitsOnOrder', isSortable: true },
    { title: 'Discontinued', key: 'discontinued', isSortable: true },
  ];

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

  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateProducts();
  }

  onPageChage(page) {
    this.query.page = page;
    this.populateProducts();
  }

  delete(productId) {
    if (confirm("Are you sure?")) {
      this.productService.delete(productId)
        .subscribe(x => {
          this.populateProducts();
        });
    }
  }

}
