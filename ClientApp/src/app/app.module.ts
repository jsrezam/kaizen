import { CategoryService } from './services/category.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { CategoryFormComponent } from './components/category-form/category-form.component';
import { CategoryViewComponent } from './components/category-view/category-view.component';
import { CategoryListComponent } from './components/category-list/category-list.component';
import { LoadingComponent } from './shared/loading/loading.component';
import { PaginationComponent } from './shared/pagination/pagination.component';
import { ProductFormComponent } from './components/product-form/product-form.component';
import { ProductListComponent } from './components/product-list/product-list.component';
import { ProductService } from './services/product.service';
import { AssignCustomersFormComponent } from './components/assign-customers-form/assign-customers-form.component';
import { CustomerService } from './services/customer.service';
import { EmployeeService } from './services/employee.service';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';
import { EmployeeFormComponent } from './components/employee-form/employee-form.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    CategoryFormComponent,
    CategoryViewComponent,
    CategoryListComponent,
    LoadingComponent,
    PaginationComponent,
    ProductFormComponent,
    ProductListComponent,
    AssignCustomersFormComponent,
    EmployeeListComponent,
    EmployeeFormComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(), // ToastrModule added
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'categories', component: CategoryListComponent },
      { path: 'categories/new', component: CategoryFormComponent },
      { path: 'categories/edit/:id', component: CategoryFormComponent },
      { path: 'products', component: ProductListComponent },
      { path: 'products/new', component: ProductFormComponent },
      { path: 'products/edit/:id', component: ProductFormComponent },
      { path: 'employees', component: EmployeeListComponent },
      { path: 'employees/new', component: EmployeeFormComponent },
      { path: 'employees/edit/:id', component: EmployeeFormComponent },
      { path: 'assign-customers', component: AssignCustomersFormComponent },
    ])
  ],
  providers: [
    CategoryService,
    ProductService,
    CustomerService,
    EmployeeService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
