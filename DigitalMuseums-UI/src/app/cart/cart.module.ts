import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatDialogModule } from "@angular/material/dialog";
import { MatTabsModule } from "@angular/material/tabs";
import { CustomFormModule } from "../core/form/custom-form.module";
import { SharedModule } from "../core/shared/shared.module";
import { CartRoutingModule } from "./cart-routing.module";
import { CartComponent } from "./components/cart/cart.component";

@NgModule({
    declarations: [CartComponent],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        MatTabsModule,
        CustomFormModule,
        MatDialogModule,
        SharedModule,
        CartRoutingModule,
      ],
  })
  export class CartModule {}