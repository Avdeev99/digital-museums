import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ConfirmDialogComponent } from 'src/app/core/form/confirm-dialog/confirm-dialog.component';
import { MuseumDetails } from '../../models/museum-details.model';
import { MuseumService } from '../../services/museum.service';
import { AddMuseumComponent } from '../add-museum/add-museum.component';
import { LinkingMuseumToUserComponent } from '../linking-museum-to-user/linking-museum-to-user.component';

@Component({
  selector: 'app-museum-list',
  templateUrl: './museum-list.component.html',
  styleUrls: ['./museum-list.component.scss']
})
export class MuseumListComponent implements OnInit {
  public museums$: Observable<Array<MuseumDetails>>;
  public isFetching: boolean = false;
  public serverError: string;

  constructor(
    private museumService: MuseumService,
    private dialog: MatDialog) { }

  public ngOnInit(): void {
    this.setMuseums();
  }

  public onAdd(): void {
    const dialogRef = this.dialog.open(AddMuseumComponent, {
      maxHeight: '90vh'
    }).afterClosed().subscribe((dialogResult: boolean) => {
        if (!dialogResult) {
          return;
        };

        this.setMuseums();
      });
  }

  public onLinkToUser(): void {
    const dialogRef = this.dialog.open(LinkingMuseumToUserComponent)
      .afterClosed().subscribe((dialogResult: boolean) => {
        if (!dialogResult) {
          return;
        };

        this.setMuseums();
      });
  }

  public onEdit(museumId: number): void {
    const dialogRef = this.dialog.open(AddMuseumComponent, {
      data: {
        museumId: museumId
      },
      maxHeight: '90vh'
    }).afterClosed().subscribe((dialogResult: boolean) => {
      if (!dialogResult) {
        return;
      };

      this.setMuseums();
    });
  }

  public onDelete(museumId: number): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().subscribe((dialogResult: boolean) => {
      if (!dialogResult) {
        return;
      }

      this.isFetching = true;

      this.museumService.delete(museumId).pipe(
        catchError((errorResponse: HttpErrorResponse) => {
          this.isFetching = false;
          this.serverError = errorResponse.error.message;
  
          throw(errorResponse);
        })
      ).subscribe(() => {
        this.isFetching = false;
        this.setMuseums();
      });
    });
  }

  private setMuseums(): void {
    this.isFetching = true;

    this.museums$ = this.museumService.getAll();

    this.museums$.subscribe(() => {
      this.isFetching = false;
      this.serverError = null;
    });
  }
}
