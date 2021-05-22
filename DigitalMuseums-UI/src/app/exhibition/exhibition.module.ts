import { ExhibitionInstructionalScreenComponent } from './components/exhibition-instructional-screen/exhibition-instructional-screen.component';
import { ExhibitionComponent } from './components/exhibition/exhibition.component';
import { ExhibitionFormComponent } from './components/exbition-form/exhibition-form.component';
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatDialogModule } from "@angular/material/dialog";
import { MatTabsModule } from "@angular/material/tabs";
import { CustomFormModule } from "../core/form/custom-form.module";
import { SharedModule } from "../core/shared/shared.module";
import { ExhibitionRoutingModule } from "./exhibition-routing.module";
import { ExhibitionStepContainerDirective } from './directives/exhibition-step-container.directive';
import { ProgressTrackerComponent } from './components/progress-tracker/progress-tracker.component';
import { ExhibitionSearchComponent } from './components/exhibition-search/exhibition-search.component';
import { ExhibitionEditingComponent } from './components/exhibition-editing/exhibition-editing.component';
import { ExhibitionListComponent } from './components/exhibition-list/exhibition-list.component';
import { RouterModule } from '@angular/router';

@NgModule({
    declarations: [
        // components
        ExhibitionFormComponent,
        ExhibitionComponent,
        ExhibitionInstructionalScreenComponent,
        ProgressTrackerComponent,
        ExhibitionSearchComponent,
        ExhibitionEditingComponent,
        ExhibitionListComponent,

        // directives
        ExhibitionStepContainerDirective,
    ],
    imports: [
        RouterModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        MatTabsModule,
        CustomFormModule,
        MatDialogModule,
        SharedModule,
        ExhibitionRoutingModule,
      ],
  })
  export class ExhibitionModule {}