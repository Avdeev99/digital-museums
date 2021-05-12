import { AddMuseumComponent } from './components/add-museum/add-museum.component';
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { MuseumDetailsComponent } from './components/museum-details/museum-details.component';
import { MuseumsComponent } from './components/museums/museums.component';
import { LinkingMuseumToUserComponent } from './components/linking-museum-to-user/linking-museum-to-user.component';
import { AuthGuard } from '../core/auth/guards/auth.guard';
import { AuthRole } from '../core/auth/models/auth-role.enum';

const routes: Routes = [
    {
      path: 'create',
      component: AddMuseumComponent,
      canActivate: [AuthGuard],
      data: {
        roles: [AuthRole.Admin],
      },
    },
    {
      path: 'update/:id',
      component: AddMuseumComponent,
      canActivate: [AuthGuard],
      data: {
        roles: [AuthRole.Admin, AuthRole.MuseumOwner],
      },
    },
    {
      path: 'search',
      component: MuseumsComponent,
      canActivate: [AuthGuard],
    },
    {
      path: 'user',
      component: LinkingMuseumToUserComponent,
      canActivate: [AuthGuard],
      data: {
        roles: [AuthRole.Admin],
      },
    },
    {
      path: ':id',
      component: MuseumDetailsComponent,
      canActivate: [AuthGuard],
    },
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  export class MuseumRoutingModule {}