import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ExhibitDetails } from '../../models/exhibit-details.model';
import { ExhibitService } from '../../services/exhibit.service';

@Component({
  selector: 'app-exhibit-details',
  templateUrl: './exhibit-details.component.html',
  styleUrls: ['./exhibit-details.component.scss']
})
export class ExhibitDetailsComponent implements OnInit {
  public exhibit: ExhibitDetails;

  private exhibitId: number;

  constructor(
    private route: ActivatedRoute,
    private exhibitService: ExhibitService,
    private router: Router,
  ) { 
    this.setExhibitId();
  }

  ngOnInit(): void {
    this.fetchExhibit();
  }

  public get exhibitImage(): string {
    return this.exhibit && this.exhibit.imagePaths.length ? this.exhibit.imagePaths[0] : null;
  }

  private setExhibitId(): void {
    this.exhibitId = this.route.snapshot.params.id;
  }

  private fetchExhibit(): void {
    this.exhibitService.get(this.exhibitId)
      .pipe(
        catchError(err => {
          this.router.navigate(['exhibit']);
          return throwError(err);
        }),
      )
      .subscribe(data => {
        this.exhibit = data;
      });
  }
}
