import { CategoryService } from 'src/app/services/category.service';
import { ProductService } from './../../services/product.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { forkJoin } from 'rxjs';
import { parseErrorsAPI } from 'src/app/common/common';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html'
})
export class ProductFormComponent implements OnInit {

  categories: any[];
  product: any = {
    category: {}
  };
  errorMessages: any[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private categoryService: CategoryService,
    private productService: ProductService,
    private toastrService: ToastrService) {

    this.route.params.subscribe(p => {
      this.product.id = +p['id'] || 0;
    });
  }

  ngOnInit(): void {
    var sources = [
      this.categoryService.getCategories(null),
    ];

    if (this.product.id)
      sources.push(this.productService.getProduct(this.product.id));

    forkJoin(sources)
      .subscribe((data: any) => {
        this.categories = data[0].items;
        if (this.product.id) {
          this.product = data[1];
        }
      }, err => {
        if (err.status == 404)
          this.router.navigate(['/']);
      });
  }

  submit() {
    var result$ = (this.product.id) ? this.productService.update(this.product) : this.productService.create(this.product);
    result$.subscribe((product: any) => {
      this.toastrService.success("Data was successfully saved.", "Success")
      this.router.navigate(['/products/'])
    }, err => {
      this.errorMessages = parseErrorsAPI(err);
    });

  }

}