import { CustomerService } from './../../services/customer.service';
import { LocationService } from './../../services/location.service';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { parseErrorsAPI } from 'src/app/common/common';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-customer-form',
  templateUrl: './customer-form.component.html'
})
export class CustomerFormComponent implements OnInit {

  countries: any = {};
  regions: any = {};
  cities: any = {};
  customer: any = {};
  errorMessages: any[];

  constructor(private route: ActivatedRoute,
    private router: Router,
    private locationService: LocationService,
    private customerService: CustomerService,
    private toastrService: ToastrService) {

    this.route.params.subscribe(p => {
      this.customer.id = +p['id'] || 0;
    });
  }

  ngOnInit(): void {

    this.populateCountries();

    if (this.customer.id) {

      this.customerService.getCustomer(this.customer.id)
        .subscribe(customer => {
          this.customer = customer;

          this.locationService.GetRegionsByCountry(this.customer.countryId)
            .subscribe(regions => {
              this.regions = regions;
            });

          this.locationService.GetCitiesByRegion(this.customer.regionId)
            .subscribe(cities => {
              this.cities = cities;
            });
        });
    }
  }

  populateCountries() {
    this.locationService.getCountries()
      .subscribe(response => this.countries = response);
  }

  populateRegionsByCountry() {
    this.locationService.GetRegionsByCountry(this.customer.countryId)
      .subscribe(response => this.regions = response);
  }

  populateCitiesByRegion() {
    this.locationService.GetCitiesByRegion(this.customer.regionId)
      .subscribe(response => this.cities = response);
  }

  onCountriesFilterChange() {
    this.populateRegionsByCountry();
  }

  onRegionsFilterChange() {
    this.populateCitiesByRegion();
  }

  createCustomer(form) {
    if (form.valid) {
      let result$ = (this.customer.id) ? this.customerService.updateCustomer(this.customer) : this.customerService.createCustomer(this.customer);
      result$.subscribe(resp => {
        this.toastrService.success("Data was successfully saved.", "Success");
        this.router.navigate(['/customers/'])
      }, err => {
        this.errorMessages = parseErrorsAPI(err);
      })
    }
  }

}
