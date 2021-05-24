import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
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

  public isFetching: boolean = false;

  constructor(
    private statisticsService: StatisticsService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.fetchStatistics();
  }

  private fetchStatistics(): void {
    this.isFetching = true;

    this.statisticsService.get()
      .pipe(
        catchError(err => {
          this.router.navigate(['/']);
          this.isFetching = false;

          return throwError(err);
        }),
      )
      .subscribe(data => {
        this.statistics = data;
        this.isFetching = false;
      });
  }

}
