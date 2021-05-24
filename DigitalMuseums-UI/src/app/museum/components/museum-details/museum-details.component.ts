import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { RelatedItem } from 'src/app/core/shared/models/related-item.model';
import { MuseumDetails } from '../../models/museum-details.model';
import { MuseumService } from '../../services/museum.service';

declare var carousel: any;

@Component({
  selector: 'app-museum-details',
  templateUrl: './museum-details.component.html',
  styleUrls: ['./museum-details.component.scss']
})
export class MuseumDetailsComponent implements OnInit {
  public museum: MuseumDetails;
  public relatedItems: Array<RelatedItem>;

  public isFetching: boolean = false;

  private museumId: number;

  constructor(
    private route: ActivatedRoute,
    private museumService: MuseumService,
    private router: Router,
  ) { 
    this.setMuseumId();
    this.initRelatedItems();
  }

  ngOnInit(): void {
    this.fetchMuseum();
  }

  public get museumImages(): string[] {
    return this.museum && this.museum.imagePaths.length ? this.museum.imagePaths: null;
  }

  private setMuseumId(): void {
    this.museumId = this.route.snapshot.params.id;
  }

  private fetchMuseum(): void {
    this.isFetching = true;

    this.museumService.get(this.museumId)
      .pipe(
        catchError(err => {
          this.router.navigate(['museum']);
          this.isFetching = false;

          return throwError(err);
        }),
      )
      .subscribe(data => {
        this.museum = data;
        this.isFetching = false;
        carousel();
      });
  }

  private initRelatedItems(): void {

    this.relatedItems = [
      {
        name: 'menu.exhibits',
        href: `/exhibit/${this.museumId}/search`
      },
      {
        name: 'menu.exhibitions',
        href: `/exhibition/${this.museumId}/search`
      },
      {
        name: 'menu.souvenirs',
        href: `/souvenir/${this.museumId}/search`
      },
    ];
  }
}
