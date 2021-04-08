import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { SouvenirDetailsComponent } from "./components/souvenir-details/souvenir-details.component";
import { SouvenirEditingComponent } from "./components/souvenir-editing/souvenir-editing.component";
import { SouvenirSearchComponent } from "./components/souvenir-search/souvenir-search.component";

const routes: Routes = [
    {
      path: 'create',
      component: SouvenirEditingComponent
    },
    {
      path: 'update/:id',
      component: SouvenirEditingComponent
    },
    {
      path: ':museumId/search',
      component: SouvenirSearchComponent
    },
    {
      path: ':id',
      component: SouvenirDetailsComponent
    },
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  export class SouvenirRoutingModule {}