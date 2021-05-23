import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "../core/auth/guards/auth.guard";
import { AuthRole } from "../core/auth/models/auth-role.enum";
import { ExhibitDetailsComponent } from "./components/exhibit-details/exhibit-details.component";
import { ExhibitListComponent } from "./components/exhibit-list/exhibit-list.component";
import { ExhibitSearchComponent } from "./components/exhibit-search/exhibit-search.component";

const routes: Routes = [
    {
      path: ':museumId/list',
      component: ExhibitListComponent,
      canActivate: [AuthGuard],
      data: {
        roles: [AuthRole.Admin, AuthRole.MuseumOwner],
      },
    },
    {
      path: ':museumId/search',
      component: ExhibitSearchComponent,
      canActivate: [AuthGuard],
    },
    {
      path: ':id',
      component: ExhibitDetailsComponent,
      canActivate: [AuthGuard],
    },
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  export class ExhibitRoutingModule {}