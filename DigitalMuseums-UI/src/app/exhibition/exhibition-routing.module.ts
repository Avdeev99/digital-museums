import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from "../core/auth/guards/auth.guard";
import { AuthRole } from "../core/auth/models/auth-role.enum";
import { ExhibitionEditingComponent } from "./components/exhibition-editing/exhibition-editing.component";
import { ExhibitionListComponent } from "./components/exhibition-list/exhibition-list.component";
import { ExhibitionSearchComponent } from "./components/exhibition-search/exhibition-search.component";
import { ExhibitionComponent } from "./components/exhibition/exhibition.component";

const routes: Routes = [
    {
        path: ':museumId/list',
        component: ExhibitionListComponent,
        canActivate: [AuthGuard],
        data: {
          roles: [AuthRole.Admin, AuthRole.MuseumOwner],
        },
    },
    {
        path: 'create',
        component: ExhibitionEditingComponent,
        canActivate: [AuthGuard],
        data: {
            roles: [AuthRole.Admin, AuthRole.MuseumOwner],
        },
    },
    {
        path: 'update/:id',
        component: ExhibitionEditingComponent,
        canActivate: [AuthGuard],
        data: {
            roles: [AuthRole.Admin, AuthRole.MuseumOwner],
        },
    },
    {
        path: ':museumId/search',
        component: ExhibitionSearchComponent,
        canActivate: [AuthGuard],
    },
    {
        path: ':id',
        component: ExhibitionComponent,
        canActivate: [AuthGuard],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ExhibitionRoutingModule {}