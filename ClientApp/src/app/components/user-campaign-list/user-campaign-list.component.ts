import { AuthService } from './../../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Campaign } from 'src/app/models/campaign';
import { Employee } from 'src/app/models/employee';
import { CampaignService } from 'src/app/services/campaign.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-campaign-list',
  templateUrl: './user-campaign-list.component.html',
  styleUrls: ['./user-campaign-list.component.css']
})
export class UserCampaignListComponent implements OnInit {

  private readonly PAGE_SIZE = 3;
  // agentUsers: any[] = [];
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

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private campaignService: CampaignService) { }

  ngOnInit(): void {
    this.populateUserCampaigns();
  }

  private populateUserCampaigns() {
    this.campaignService._getUserCampaigns(this.query)
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
    this.populateUserCampaigns();
  }

  onPageChage(page) {
    this.query.page = page;
    this.populateUserCampaigns();
  }

  isExpired(finishDate) {
    if (new Date(finishDate) < new Date())
      return true;
    return false;
  }

}
