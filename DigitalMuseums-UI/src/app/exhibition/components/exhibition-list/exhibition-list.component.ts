import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ConfirmDialogComponent } from 'src/app/core/form/confirm-dialog/confirm-dialog.component';
import { Exhibition } from '../../models/exhibition.model';
import { ExhibitionService } from '../../services/exhibition.service';
import { ExhibitionEditingComponent } from '../exhibition-editing/exhibition-editing.component';

@Component({
  selector: 'app-exhibition-list',
  templateUrl: './exhibition-list.component.html',
  styleUrls: ['./exhibition-list.component.scss']
})
export class ExhibitionListComponent implements OnInit {
  public museumId: number;
  public exhibitions$: Observable<Array<Exhibition>>;
  public isFetching: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private exhibitionsService: ExhibitionService,
    private dialog: MatDialog) { }

  public ngOnInit(): void {
    this.setMuseumId();
    this.setMuseumExhibitions();
  }

  public onAdd(): void {
    const dialogRef = this.dialog.open(ExhibitionEditingComponent)
      .afterClosed().subscribe((dialogResult: boolean) => {
        if (!dialogResult) {
          return;
        };

        this.setMuseumExhibitions();
      });
  }

  public onEdit(exhibitionId: number): void {
    const dialogRef = this.dialog.open(ExhibitionEditingComponent, {
      data: {
        exhibitionId: exhibitionId
      }
    }).afterClosed().subscribe((dialogResult: boolean) => {
      if (!dialogResult) {
        return;
      };

      this.setMuseumExhibitions();
    });
  }

  public onDelete(exhibitionId: number): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().subscribe((dialogResult: boolean) => {
      if (!dialogResult) {
        return;
      }

      this.isFetching = true;

      this.exhibitionsService.delete(exhibitionId).subscribe(() => {
        this.isFetching = false;
        this.setMuseumExhibitions();
      });
    });
  }

  private setMuseumId(): void {
    this.museumId = this.route.snapshot.params.museumId;
  }

  private setMuseumExhibitions(): void {
    this.isFetching = true;

    this.exhibitions$ = this.exhibitionsService.getFiltered({
      museumId: this.museumId
    });

    this.exhibitions$.subscribe(() => {
      this.isFetching = false;
    });
  }
}
