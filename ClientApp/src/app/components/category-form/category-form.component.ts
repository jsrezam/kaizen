import { CategoryService } from './../../services/category.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-category-form',
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.css']
})
export class CategoryFormComponent implements OnInit {
  category: any = {};

  constructor(
    private route: ActivatedRoute, //Read routes parameters
    private router: Router,
    private categoryService: CategoryService,
    private toastrService: ToastrService) {

    route.params.subscribe(p => {
      this.category.id = +p['id'] || 0;
    });
  }

  ngOnInit(): void {
    if (this.category.id)
      this.categoryService.getCategory(this.category.id)
        .subscribe((resp) => {
          this.category = resp
        });
  }

  submit() {
    var result$ = (this.category.id) ? this.categoryService.update(this.category) : this.categoryService.create(this.category);
    result$.subscribe((category: any) => {
      this.toastrService.success("Data was sucessfully saved.", "Success", {
        onActivateTick: true
      })
      this.router.navigate(['/categories/'])
    });
  }

}
