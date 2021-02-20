import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MuseumDetails } from '../../models/museum-details.model';
import { MuseumService } from '../../services/museum.service';

@Component({
  selector: 'app-museum-details',
  templateUrl: './museum-details.component.html',
  styleUrls: ['./museum-details.component.scss']
})
export class MuseumDetailsComponent implements OnInit {
  public museum: MuseumDetails;

  private museumId: number;

  constructor(
    private route: ActivatedRoute,
    private museumService: MuseumService,
    private router: Router,
  ) { 
    this.setMuseumId();
  }

  ngOnInit(): void {
    this.fetchMuseum();
  }

  public get museumImage(): string {
    return this.museum && this.museum.imagePaths.length ? this.museum.imagePaths[0] : null;
  }

  private setMuseumId(): void {
    this.museumId = this.route.snapshot.params.id;
  }

  private fetchMuseum(): void {
    this.museumService.get(this.museumId)
      .pipe(
        catchError(err => {
          this.router.navigate(['museum']);
          return throwError(err);
        }),
      )
      .subscribe(data => {
        this.museum = data;
      });
  }
}
