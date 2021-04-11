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
    { title: 'Id' },
    { title: 'Name', key: 'name', isSortable: true },
    { title: 'Description', key: 'description', isSortable: true },
  ];

  queryResult: any = {};
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
    this.populateCategories();
  }

  onPageChage(page) {
    this.query.page = page;
    this.populateCategories();
  }

  delete(categoryId) {
    if (confirm("Are you sure?")) {
      this.categoryService.delete(categoryId)
        .subscribe(x => {
          this.populateCategories();
        });
    }
  }

}
