import { CampaignService } from './../../services/campaign.service';
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
  agent: any = {};
  campaign: any = {};
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };
  columns = [
    { title: 'Campaign' },
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

  private populateAgentCampaigns(userId) {
    this.campaignService.getAgentCampaigns(this.query, userId)
      .subscribe((result: any) => {
        this.queryResult = result
      });
  }

  onUserChange() {
    this.populateAgentCampaigns(this.agent.id);
  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateAgentCampaigns(this.agent.id);
  }

  onPageChage(page) {
    this.query.page = page;
    this.populateAgentCampaigns(this.agent.id);
  }

  inactivate(campaign) {
    if (confirm("Are you sure?")) {
      this.campaign = campaign;
      this.campaign.isActive = false;
      this.campaignService.inactivateCampaign(campaign)
        .subscribe(resp => {
          this.populateAgentCampaigns(this.agent.id);
        });
    }
  }

  isExpired(finishDate) {
    if (new Date(finishDate) < new Date())
      return true;
    return false;
  }

}
