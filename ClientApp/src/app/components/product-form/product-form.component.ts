import { CategoryService } from 'src/app/services/category.service';
import { ProductService } from './../../services/product.service';
import { Product } from './../../models/product';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit {
  categories: any[];
  product: Product = {
    category: {}
  }
  constructor(
    private route: ActivatedRoute, //Read routes parameters
    private router: Router,
    private categoryService: CategoryService,
    private productService: ProductService,
    private toastrService: ToastrService) {

    route.params.subscribe(p => {
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
      this.toastrService.success("Data was sucessfully saved.", "Success", {
        onActivateTick: true
      })
      this.router.navigate(['/products/'])
    });

  }

}