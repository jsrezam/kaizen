import { Component, OnInit } from '@angular/core';
import { CampaignService } from 'src/app/services/campaign.service';

@Component({
  selector: 'app-agent-campaign-list',
  templateUrl: './agent-campaign-list.component.html',
  styleUrls: ['./agent-campaign-list.component.css']
})
export class AgentCampaignListComponent implements OnInit {

  private readonly PAGE_SIZE = 3;
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

  constructor(
    private campaignService: CampaignService) { }

  ngOnInit(): void {
    this.populateAgentValidCampaigns();
  }

  private populateAgentValidCampaigns() {
    this.campaignService.getAgentValidCampaigns(this.query)
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
    this.populateAgentValidCampaigns();
  }

  onPageChage(page) {
    this.query.page = page;
    this.populateAgentValidCampaigns();
  }

  isExpired(finishDate) {
    if (new Date(finishDate) < new Date())
      return true;
    return false;
  }

}
