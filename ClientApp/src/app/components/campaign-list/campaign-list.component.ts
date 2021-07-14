import { CampaignService } from './../../services/campaign.service';
import { UserService } from './../../services/user.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-campaign-list',
  templateUrl: './campaign-list.component.html'
})
export class CampaignListComponent implements OnInit {
  private readonly PAGE_SIZE = 3;

  columns = [
    { title: 'Campaign Id', key: 'id', isSortable: false, searchable: true, defaultSearch: true },
    { title: 'Start Date', key: 'startDate', isSortable: false, searchable: false },
    { title: 'Finish Date', key: 'finishDate', isSortable: false, searchable: false },
    { title: 'State', key: 'isActive', isSortable: false, searchable: false },
    { title: 'Progress', key: 'progress', isSortable: false, searchable: false }
  ];

  searchOption: any;
  searchPlaceholder: string;

  agents: any[] = [];
  agent: any = {};

  campaign: any = {};
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };

  constructor(private userService: UserService,
    private campaignService: CampaignService) { }

  ngOnInit(): void {
    this.populateActiveAgents();
  }

  private populateActiveAgents() {
    this.userService.getActiveAgents()
      .subscribe((result: any) => {
        this.agents = result;
      });
  }

  private populateAgentCampaigns() {
    this.campaignService.getAgentCampaigns(this.query, this.agent.id)
      .subscribe((result: any) => {
        this.queryResult = result;
      });
  }

  onPageChage(page) {
    this.query.page = page;
    this.populateAgentCampaigns();
  }

  setPlaceholderSearch() {
    if (!this.searchOption) {
      let defaultColumnSearch = this.getDefaultColumnSearch();
      this.searchPlaceholder = "Search by " + defaultColumnSearch.title;
      return this.searchPlaceholder;
    }
    let columnSearch = this.columns.find(c => c.key === this.searchOption);
    this.searchPlaceholder = "Search by " + columnSearch.title;
    return this.searchPlaceholder;
  }

  filterSearchOptions() {
    this.setPlaceholderSearch();
    return this.columns.filter(c => c.searchable);
  }

  getDefaultColumnSearch() {
    return this.columns.find(c => c.defaultSearch);
  }

  resetFilter() {
    this.query = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };
  }

  onFilterChange() {
    this.setPlaceholderSearch();
    this.resetFilter();
  }

  search(querySearch) {

    this.resetFilter();

    if (!this.searchOption) {
      let defaultColumnSearch = this.getDefaultColumnSearch();
      this.query[defaultColumnSearch.key] = querySearch;
    } else {
      this.query[this.searchOption] = querySearch;
    }

    this.populateAgentCampaigns();
  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateAgentCampaigns();
  }

  onAgentFilterChange() {
    this.resetFilter();
    this.populateAgentCampaigns();
  }

  isExpired(finishDate) {
    if (new Date(finishDate) < new Date())
      return true;
    return false;
  }

}
