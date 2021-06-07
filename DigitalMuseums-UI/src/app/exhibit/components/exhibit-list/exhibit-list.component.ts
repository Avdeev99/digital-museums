import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ConfirmDialogComponent } from 'src/app/core/form/confirm-dialog/confirm-dialog.component';
import { ExhibitDetails } from '../../models/exhibit-details.model';
import { Exhibit } from '../../models/exhibit.model';
import { ExhibitService } from '../../services/exhibit.service';
import { ExhibitEditingComponent } from '../exhibit-editing/exhibit-editing.component';

@Component({
  selector: 'app-exhibit-list',
  templateUrl: './exhibit-list.component.html',
  styleUrls: ['./exhibit-list.component.scss']
})
export class ExhibitListComponent implements OnInit {
  public museumId: number;
  public exhibits$: Observable<Array<ExhibitDetails>>;
  public isFetching: boolean = false;
  public serverError: string;

  constructor(
    private route: ActivatedRoute,
    private exhibitService: ExhibitService,
    private dialog: MatDialog) { }

  public ngOnInit(): void {
    this.setMuseumId();
    this.setMuseumExhibits();
  }

  public onAdd(): void {
    const dialogRef = this.dialog.open(ExhibitEditingComponent, {
      minWidth: '50vw',
    }).afterClosed().subscribe((dialogResult: boolean) => {
        if (!dialogResult) {
          return;
        };

        this.setMuseumExhibits();
      });
  }

  public onEdit(exhibitId: number): void {
    const dialogRef = this.dialog.open(ExhibitEditingComponent, {
      data: {
        exhibitId: exhibitId
      },
      minWidth: '50vw',
    }).afterClosed().subscribe((dialogResult: boolean) => {
      if (!dialogResult) {
        return;
      };

      this.setMuseumExhibits();
    });
  }

  public onDelete(exhibitId: number): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().subscribe((dialogResult: boolean) => {
      if (!dialogResult) {
        return;
      }

      this.isFetching = true;

      this.exhibitService.delete(exhibitId).pipe(
        catchError((errorResponse: HttpErrorResponse) => {
          this.isFetching = false;
          this.serverError = errorResponse.error.message;

          throw (errorResponse);
        })
      ).subscribe(() => {
        this.isFetching = false;
        this.setMuseumExhibits();
      });
    });
  }

  private setMuseumId(): void {
    this.museumId = this.route.snapshot.params.museumId;
  }

  private setMuseumExhibits(): void {
    this.isFetching = true;

    this.exhibits$ = this.exhibitService.getFiltered({
      museumId: this.museumId
    });

    this.exhibits$.subscribe(() => {
      this.isFetching = false;
      this.serverError = null;
    });
  }
}
