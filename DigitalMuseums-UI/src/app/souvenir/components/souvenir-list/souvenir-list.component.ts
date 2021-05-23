import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { ConfirmDialogComponent } from 'src/app/core/form/confirm-dialog/confirm-dialog.component';
import { SouvenirDetails } from '../../models/souvenir-details.model';
import { SouvenirService } from '../../services/souvenir.service';
import { SouvenirEditingComponent } from '../souvenir-editing/souvenir-editing.component';

@Component({
  selector: 'app-souvenir-list',
  templateUrl: './souvenir-list.component.html',
  styleUrls: ['./souvenir-list.component.scss']
})
export class SouvenirListComponent implements OnInit {
  public museumId: number;
  public souvenirs$: Observable<Array<SouvenirDetails>>;
  public isFetching: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private souvenirService: SouvenirService,
    private dialog: MatDialog) { }

  public ngOnInit(): void {
    this.setMuseumId();
    this.setMuseumSouvenirs();
  }

  public onAdd(): void {
    const dialogRef = this.dialog.open(SouvenirEditingComponent)
      .afterClosed().subscribe((dialogResult: boolean) => {
        if (!dialogResult) {
          return;
        };

        this.setMuseumSouvenirs();
      });
  }

  public onEdit(souvenirId: number): void {
    const dialogRef = this.dialog.open(SouvenirEditingComponent, {
      data: {
        souvenirId: souvenirId
      }
    }).afterClosed().subscribe((dialogResult: boolean) => {
      debugger;
      if (!dialogResult) {
        return;
      };

      this.setMuseumSouvenirs();
    });
  }

  public onDelete(souvenirId: number): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().subscribe((dialogResult: boolean) => {
      if (!dialogResult) {
        return;
      }

      this.isFetching = true;

      this.souvenirService.delete(souvenirId).subscribe(() => {
        this.isFetching = false;
        this.setMuseumSouvenirs();
      });
    });
  }

  private setMuseumId(): void {
    this.museumId = this.route.snapshot.params.museumId;
  }

  private setMuseumSouvenirs(): void {
    this.isFetching = true;

    this.souvenirs$ = this.souvenirService.getFiltered({
      museumId: this.museumId
    });

    this.souvenirs$.subscribe(() => {
      this.isFetching = false;
    });
  }
}
