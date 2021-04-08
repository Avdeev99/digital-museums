import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ExhibitDetailsComponent } from "./components/exhibit-details/exhibit-details.component";
import { ExhibitEditingComponent } from "./components/exhibit-editing/exhibit-editing.component";
import { ExhibitSearchComponent } from "./components/exhibit-search/exhibit-search.component";

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
      component: ExhibitSearchComponent
    },
    {
      path: ':id',
      component: ExhibitDetailsComponent
    },
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  export class ExhibitRoutingModule {}