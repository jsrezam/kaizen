import { forkJoin } from 'rxjs';
import { ReportService } from '../../services/report.service';
import { Component, OnInit } from '@angular/core';
import { generateColors } from 'src/app/common/common';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  public pieChartLabels: any[] = [];
  public pieChartData: any[] = [];
  public pieChartType = 'pie';
  pieChartColor: any = [
    {
      backgroundColor: [] = []
    }
  ]

  public barChartLabels: any[] = [];
  barChartData = [
    {
      label: 'Total Sales',
      data: [],
    }
  ];
  public barChartType = 'bar';
  public barChartLegend = true;
  public barChartOptions = {
    scaleShowVerticalLines: false,
    responsive: true
  };

  topCustomers: any[] = [];
  topProducts: any[] = [];
  topAgent: any = {};

  constructor(private reportService: ReportService) { }

  ngOnInit(): void {
    var sources = [
      this.reportService.getTotalSalesByAgent(),
      this.reportService.getTotalSalesByMonth(),
      this.reportService.getTopCustomers(),
      this.reportService.getTopSellingProducts(),
      this.reportService.getTopAgent()
    ]

    forkJoin(sources)
      .subscribe((data: any) => {
        this.pieChartColor[0].backgroundColor = (generateColors(data[0].length));
        data[0].forEach(element => {
          this.pieChartLabels.push(element.agentName);
          this.pieChartData.push(element.totalSales);
        });
        data[1].forEach(element => {
          this.barChartLabels.push(element.month);
          this.barChartData[0].data.push(element.totalSales)
        });
        this.topCustomers = data[2];
        this.topProducts = data[3];
        this.topAgent = data[4];
      })
  }

}
