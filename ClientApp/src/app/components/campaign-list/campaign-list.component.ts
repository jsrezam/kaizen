import { Campaign } from 'src/app/models/campaign';
import { CampaignService } from './../../services/campaign.service';
import { Employee } from './../../models/employee';
import { UserService } from './../../services/user.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-campaign-list',
  templateUrl: './campaign-list.component.html',
  styleUrls: ['./campaign-list.component.css']
})
export class CampaignListComponent implements OnInit {
  private readonly PAGE_SIZE = 3;
  agentUsers: any[] = [];
  employee: Employee = {};
  campaign: Campaign = {};
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };
  columns = [
    { title: 'Campaign' },
    // { title: 'UserId', key: 'userId', isSortable: false },
    { title: 'Start Date', key: 'startDate', isSortable: false },
    { title: 'Finish Date', key: 'finishDate', isSortable: false },
    { title: 'State', key: 'isActive', isSortable: false },
    { title: 'Progress', key: 'progress', isSortable: false }
  ];

  constructor(private userService: UserService,
    private campaignService: CampaignService) { }

  ngOnInit(): void {
    this.userService.getAgentUsers()
      .subscribe((result: any) => {
        this.agentUsers = result;
      });
  }

  private populateUserCampaigns(userId) {
    this.campaignService.getUserCampaigns(this.query, userId)
      .subscribe((result: any) => {
        this.queryResult = result
      });
  }

  onUserChange() {
    // console.log(this.employee.id);
    this.populateUserCampaigns(this.employee.id);

  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateUserCampaigns(this.employee.id);
  }

  onPageChage(page) {
    this.query.page = page;
    this.populateUserCampaigns(this.employee.id);
  }

  inactivate(campaign) {
    if (confirm("Are you sure?")) {
      this.campaign = campaign;
      this.campaign.isActive = false;
      this.campaignService.inactivateCampaign(campaign)
        .subscribe(resp => {
          this.populateUserCampaigns(this.employee.id);
        });
    }

  }

  isExpired(finishDate) {
    if (new Date(finishDate) < new Date())
      return true;
    return false;
  }

}
