import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ExhibitEditingComponent } from "../exhibit/components/exhibit-editing/exhibit-editing.component";
import { ExhibitionSearchComponent } from "./components/exhibition-search/exhibition-search.component";
import { ExhibitionComponent } from "./components/exhibition/exhibition.component";

const routes: Routes = [
  {
    path: 'create',
    component: ExhibitEditingComponent
  },
  {
    path: 'update/:id',
    component: ExhibitEditingComponent
  },
  {
    path: ':museumId/search',
    component: ExhibitionSearchComponent
  },
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