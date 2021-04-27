import { Employee } from 'src/app/models/employee';
import { CampaignService } from './../../services/campaign.service';
import { UserService } from './../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Campaign } from 'src/app/models/campaign';
import { CampaignDetailService } from 'src/app/services/campaignDetail.service';

@Component({
  selector: 'app-campaign-detail',
  templateUrl: './campaign-detail.component.html',
  styleUrls: ['./campaign-detail.component.css']
})
export class CampaignDetailComponent implements OnInit {
  private readonly PAGE_SIZE = 3;
  columns = [
    { title: 'Id' },
    { title: 'Customer First Name', key: 'campaign.user.firstname', isSortable: true },
    { title: 'Customer Last Name', key: 'campaign.user.lastname', isSortable: true },
    { title: 'Calls Number', key: 'name', isSortable: true },
    { title: 'Call Duration', key: 'quantityPerUnit', isSortable: true },
    { title: 'Status Call', key: 'unitPrice', isSortable: true }
  ];

  user: Employee = {};
  campaign: Campaign = {};
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };

  constructor(
    private route: ActivatedRoute, //Read routes parameters
    private router: Router,
    private campaignService: CampaignService,
    private campaignDetailService: CampaignDetailService,
    private userService: UserService) {

    this.route.params.subscribe(c => {
      this.campaign.id = +c['id'] || 0;
    });

  }

  ngOnInit(): void {

    this.userService.getUserByCampaign(this.campaign.id)
      .subscribe((response: any) => {
        this.user = response;
      });
    this.populateCampaignDetail();
  }

  populateCampaignDetail() {
    this.campaignDetailService.getCampaignDetail(this.query, this.campaign.id)
      .subscribe((response: any) => {
        this.queryResult = response;
      });
  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateCampaignDetail();
  }

  onPageChage(page) {
    this.query.page = page;
    this.populateCampaignDetail();
  }
}