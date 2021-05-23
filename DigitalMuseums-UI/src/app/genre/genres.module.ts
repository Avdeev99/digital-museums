import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatDialogModule } from "@angular/material/dialog";
import { MatTabsModule } from "@angular/material/tabs";
import { TranslateModule } from "@ngx-translate/core";
import { CustomFormModule } from "../core/form/custom-form.module";
import { SharedModule } from "../core/shared/shared.module";
import { GenreEditingComponent } from "./components/genre-editing/genre-editing.component";
import { GenreListComponent } from "./components/genre-list/genre-list.component";
import { GenresRoutingModule } from "./genres-routing.module";

@NgModule({
    declarations: [
      GenreListComponent,
      GenreEditingComponent,
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        MatTabsModule,
        CustomFormModule,
        MatDialogModule,
        SharedModule,
        TranslateModule,
        GenresRoutingModule
      ],
  })
  export class GenresModule {}
  