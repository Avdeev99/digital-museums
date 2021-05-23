import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from "../core/auth/guards/auth.guard";
import { AuthRole } from "../core/auth/models/auth-role.enum";
import { GenreListComponent } from "./components/genre-list/genre-list.component";

const routes: Routes = [
    {
      path: '',
      component: GenreListComponent,
      canActivate: [AuthGuard],
      data: {
        roles: [AuthRole.Admin],
      },
    },
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  export class GenresRoutingModule {}