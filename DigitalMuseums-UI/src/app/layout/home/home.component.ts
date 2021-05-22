import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { StatisticsDetails } from '../../statistics/models/statistics-details.model';
import { StatisticsService } from '../../statistics/services/statistics.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  public statistics: StatisticsDetails;

  constructor(
    private statisticsService: StatisticsService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.fetchStatistics();
  }

  private fetchStatistics(): void {
    this.statisticsService.get()
      .pipe(
        catchError(err => {
          this.router.navigate(['/']);
          return throwError(err);
        }),
      )
      .subscribe(data => {
        this.statistics = data;
      });
  }

}
