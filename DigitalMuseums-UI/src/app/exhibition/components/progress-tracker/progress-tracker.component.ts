import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ExhibitionService } from '../../services/exhibition.service';

@Component({
  selector: 'app-progress-tracker',
  templateUrl: './progress-tracker.component.html',
  styleUrls: ['./progress-tracker.component.scss']
})
export class ProgressTrackerComponent implements OnInit {
  stepsTotal: number;
  currentStep$: Observable<number>;
  width$: Observable<string>;

  constructor(private exhibitionService: ExhibitionService) {}

  ngOnInit(): void {
      this.stepsTotal = this.exhibitionService.getStepsTrackerCount();
      this.currentStep$ = this.exhibitionService.getCurrentStepsTrackerNumber();
      this.width$ = this.currentStep$.pipe(map(step => `${(step / this.stepsTotal) * 100}%`));
  }
}
