import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatDialogModule } from "@angular/material/dialog";
import { MatTabsModule } from "@angular/material/tabs";
import { CustomFormModule } from "../core/form/custom-form.module";
import { SharedModule } from "../core/shared/shared.module";
import { SouvenirRoutingModule } from "./souvenir-routing.module";
import { SouvenirDetailsComponent } from './components/souvenir-details/souvenir-details.component';
import { SouvenirEditingComponent } from './components/souvenir-editing/souvenir-editing.component';
import { SouvenirSearchComponent } from './components/souvenir-search/souvenir-search.component';

@NgModule({
    declarations: [
        SouvenirDetailsComponent,
        SouvenirEditingComponent,
        SouvenirSearchComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        MatTabsModule,
        CustomFormModule,
        MatDialogModule,
        SharedModule,
        SouvenirRoutingModule,
      ],
  })
  export class SouvenirModule {}
  