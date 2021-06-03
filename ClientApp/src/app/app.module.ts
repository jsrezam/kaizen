import { OrderService } from './services/order.service';
import { LocationService } from './services/location.service';
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
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { CategoryFormComponent } from './components/category-form/category-form.component';
import { CategoryListComponent } from './components/category-list/category-list.component';
import { LoadingComponent } from './shared/loading/loading.component';
import { PaginationComponent } from './shared/pagination/pagination.component';
import { ProductFormComponent } from './components/product-form/product-form.component';
import { ProductListComponent } from './components/product-list/product-list.component';
import { ProductService } from './services/product.service';
import { CustomerService } from './services/customer.service';


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
import { AgentCampaignListComponent } from './components/agent-campaign-list/agent-campaign-list.component';
import { OrderFormComponent } from './components/order-form/order-form.component';
import { CustomerFormComponent } from './components/customer-form/customer-form.component';
import { CustomerListComponent } from './components/customer-list/customer-list.component';
import { UserListComponent } from './components/users-list/user-list.component';
import { OrderListComponent } from './components/order-list/order-list.component';
import { OrderDetailComponent } from './components/order-detail/order-detail.component';
import { ReportsComponent } from './components/reports/reports.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CategoryFormComponent,
    CategoryListComponent,
    LoadingComponent,
    PaginationComponent,
    ProductFormComponent,
    ProductListComponent,
    LoginComponent,
    SignupFormComponent,
    LogoutComponent,
    NonAuthorizedComponent,
    CampaignFormComponent,
    CampaignListComponent,
    CampaignDetailComponent,
    AgentCampaignListComponent,
    OrderFormComponent,
    CustomerFormComponent,
    CustomerListComponent,
    UserListComponent,
    OrderListComponent,
    OrderDetailComponent,
    ReportsComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(), // ToastrModule added
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'categories', component: CategoryListComponent, canActivate: [AuthGuard] },
      { path: 'categories/new', component: CategoryFormComponent, canActivate: [AuthGuard] },
      { path: 'categories/edit/:id', component: CategoryFormComponent, canActivate: [AuthGuard] },
      { path: 'products', component: ProductListComponent, canActivate: [AuthGuard] },
      { path: 'products/new', component: ProductFormComponent, canActivate: [AuthGuard] },
      { path: 'products/edit/:id', component: ProductFormComponent, canActivate: [AuthGuard] },


      { path: 'login', component: LoginComponent },
      { path: 'sign-up', component: SignupFormComponent },
      { path: 'non-authorized', component: NonAuthorizedComponent },

      { path: 'campaigns', component: CampaignListComponent, canActivate: [AuthGuard] },
      { path: 'campaigns/new', component: CampaignFormComponent, canActivate: [AuthGuard] },
      { path: 'campaigns-detail/:id', component: CampaignDetailComponent },

      { path: 'agent-campaigns', component: AgentCampaignListComponent },


      { path: 'customers', component: CustomerListComponent },
      { path: 'customers/new', component: CustomerFormComponent },
      { path: 'customers/edit/:id', component: CustomerFormComponent },

      { path: 'users', component: UserListComponent, canActivate: [AuthGuard] },

      { path: 'orders', component: OrderListComponent },
      { path: 'orders/new', component: OrderFormComponent },
      { path: 'orders-detail/:id', component: OrderDetailComponent },



    ]), NgbModule
  ],
  providers: [
    CategoryService,
    ProductService,
    CustomerService,
    UserService,
    AuthService,
    AuthGuard,
    CampaignService,
    CampaignDetailService,
    LocationService,
    OrderService,
    { provide: HTTP_INTERCEPTORS, useClass: SecurityInterceptorService, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
