import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatDialogModule } from "@angular/material/dialog";
import { MatTabsModule } from "@angular/material/tabs";
import { CustomFormModule } from "../core/form/custom-form.module";
import { SharedModule } from "../core/shared/shared.module";
import { GenreListComponent } from "./genre-list/genre-list.component";
import { GenresRoutingModule } from "./genres-routing.module";

@NgModule({
    declarations: [
      GenreListComponent,
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        MatTabsModule,
        CustomFormModule,
        MatDialogModule,
        SharedModule,
        GenresRoutingModule
      ],
  })
  export class GenresModule {}
  