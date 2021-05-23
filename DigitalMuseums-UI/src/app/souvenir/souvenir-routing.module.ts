import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "../core/auth/guards/auth.guard";
import { AuthRole } from "../core/auth/models/auth-role.enum";
import { SouvenirDetailsComponent } from "./components/souvenir-details/souvenir-details.component";
import { SouvenirEditingComponent } from "./components/souvenir-editing/souvenir-editing.component";
import { SouvenirListComponent } from "./components/souvenir-list/souvenir-list.component";
import { SouvenirSearchComponent } from "./components/souvenir-search/souvenir-search.component";

const routes: Routes = [
    {
        path: ':museumId/list',
        component: SouvenirListComponent,
        canActivate: [AuthGuard],
        data: {
          roles: [AuthRole.Admin, AuthRole.MuseumOwner],
        },
    },
    {
        path: 'create',
        component: SouvenirEditingComponent,
        canActivate: [AuthGuard],
        data: {
            roles: [AuthRole.Admin, AuthRole.MuseumOwner],
        },
    },
    {
        path: 'update/:id',
        component: SouvenirEditingComponent,
        canActivate: [AuthGuard],
        data: {
            roles: [AuthRole.Admin, AuthRole.MuseumOwner],
        },
    },
    {
        path: ':museumId/search',
        component: SouvenirSearchComponent,
        canActivate: [AuthGuard],
    },
    {
        path: ':id',
        component: SouvenirDetailsComponent,
        canActivate: [AuthGuard],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class SouvenirRoutingModule { }