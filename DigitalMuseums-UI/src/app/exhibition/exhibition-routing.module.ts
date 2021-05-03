import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ExhibitionEditingComponent } from "./components/exhibition-editing/exhibition-editing.component";
import { ExhibitionSearchComponent } from "./components/exhibition-search/exhibition-search.component";
import { ExhibitionComponent } from "./components/exhibition/exhibition.component";

const routes: Routes = [
  {
    path: 'create',
    component: ExhibitionEditingComponent
  },
  {
    path: 'update/:id',
    component: ExhibitionEditingComponent
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