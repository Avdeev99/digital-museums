import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ConfirmDialogComponent } from 'src/app/core/form/confirm-dialog/confirm-dialog.component';
import { GenreService } from 'src/app/core/shared/services/genre.service';
import { GenreDetails } from '../../models/genre-details.model';
import { GenreEditingComponent } from '../genre-editing/genre-editing.component';

@Component({
  selector: 'app-genre-list',
  templateUrl: './genre-list.component.html',
  styleUrls: ['./genre-list.component.scss']
})
export class GenreListComponent implements OnInit {
  public genres$: Observable<Array<GenreDetails>>;
  public isFetching: boolean = false;
  public serverError: string;

  constructor(
    private genreService: GenreService,
    private dialog: MatDialog) { }

  public ngOnInit(): void {
    this.setGenres();
  }

  public onAdd(): void {
    const dialogRef = this.dialog.open(GenreEditingComponent)
      .afterClosed().subscribe((dialogResult: boolean) => {
        if (!dialogResult) {
          return;
        };

        this.setGenres();
      });
  }

  public onEdit(genreId: number): void {
    const dialogRef = this.dialog.open(GenreEditingComponent, {
      data: {
        genreId: genreId
      }
    }).afterClosed().subscribe((dialogResult: boolean) => {
      if (!dialogResult) {
        return;
      };

      this.setGenres();
    });
  }

  public onDelete(genreId: number): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().subscribe((dialogResult: boolean) => {
      if (!dialogResult) {
        return;
      }

      this.isFetching = true;

      this.genreService.delete(genreId).pipe(
        catchError((errorResponse: HttpErrorResponse) => {
          this.isFetching = false;
          this.serverError = errorResponse.error.message;

          throw(errorResponse);
        })
      ).subscribe(() => {
        this.isFetching = false;
        this.setGenres();
      });
    });
  }

  private setGenres(): void {
    this.isFetching = true;

    this.genres$ = this.genreService.getAll();

    this.genres$.subscribe(() => {
      this.isFetching = false;
      this.serverError = null;
    });
  }
}
