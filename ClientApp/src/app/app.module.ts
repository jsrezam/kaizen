import { AuthGuard } from './services/auth-guard.service';
import { AuthService } from './services/auth.service';
import { CategoryService } from './services/category.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
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
// import { EmployeeFormComponent } from './components/employee-form/employee-form.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LoginComponent } from './components/login/login.component';
import { SignupFormComponent } from './components/signup-form/signup-form.component';
import { LogoutComponent } from './components/logout/logout.component';
import { NonAuthorizedComponent } from './components/non-authorized/non-authorized.component';
import { CampaignFormComponent } from './components/campaign-form/campaign-form.component';
import { CampaignListComponent } from './components/campaign-list/campaign-list.component';
import { UserService } from './services/user.service';
import { SecurityInterceptorService } from './services/security-interceptor.service';
import { CampaignService } from './services/campaign.service';
import { CampaignDetailComponent } from './components/campaign-detail/campaign-detail.component';
import { CampaignDetailService } from './services/campaignDetail.service';
import { UserCampaignListComponent } from './components/user-campaign-list/user-campaign-list.component';
import { UserCampaignDetailComponent } from './components/user-campaign-detail/user-campaign-detail.component';
import { OrderFormComponent } from './components/order-form/order-form.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
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
    // EmployeeFormComponent,
    LoginComponent,
    SignupFormComponent,
    LogoutComponent,
    NonAuthorizedComponent,
    CampaignFormComponent,
    CampaignListComponent,
    CampaignDetailComponent,
    UserCampaignListComponent,
    UserCampaignDetailComponent,
    OrderFormComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(), // ToastrModule added
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'categories', component: CategoryListComponent, canActivate: [AuthGuard] },
      { path: 'categories/new', component: CategoryFormComponent, canActivate: [AuthGuard] },
      { path: 'categories/edit/:id', component: CategoryFormComponent, canActivate: [AuthGuard] },
      { path: 'products', component: ProductListComponent, canActivate: [AuthGuard] },
      { path: 'products/new', component: ProductFormComponent, canActivate: [AuthGuard] },
      { path: 'products/edit/:id', component: ProductFormComponent, canActivate: [AuthGuard] },
      { path: 'employees', component: EmployeeListComponent, canActivate: [AuthGuard] },
      // { path: 'employees/new', component: EmployeeFormComponent, canActivate: [AuthGuard] },
      // { path: 'employees/edit/:id', component: EmployeeFormComponent, canActivate: [AuthGuard] },
      { path: 'login', component: LoginComponent },
      { path: 'sign-up', component: SignupFormComponent },
      { path: 'non-authorized', component: NonAuthorizedComponent },
      { path: 'campaigns', component: CampaignListComponent },
      { path: 'campaigns/new', component: CampaignFormComponent },
      { path: 'campaigns-detail/:id', component: CampaignDetailComponent },

      { path: 'usr-campaigns', component: UserCampaignListComponent },
      { path: 'usr-campaigns-detail/:id', component: UserCampaignDetailComponent },

      { path: 'orders/new', component: OrderFormComponent },

      { path: 'assign-customers', component: AssignCustomersFormComponent },
    ]), NgbModule
  ],
  providers: [
    CategoryService,
    ProductService,
    CustomerService,
    EmployeeService,
    UserService,
    AuthService,
    AuthGuard,
    CampaignService,
    CampaignDetailService,
    { provide: HTTP_INTERCEPTORS, useClass: SecurityInterceptorService, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
