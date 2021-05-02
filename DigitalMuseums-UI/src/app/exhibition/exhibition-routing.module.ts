import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ExhibitionComponent } from "./components/exhibition/exhibition.component";

const routes: Routes = [
    {
      path: ':id',
      component: ExhibitionComponent
    },
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  export class ExhibitionRoutingModule {}