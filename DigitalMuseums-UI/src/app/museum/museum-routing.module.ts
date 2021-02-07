import { AddMuseumComponent } from './components/add-museum/add-museum.component';
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { MuseumDetailsComponent } from './components/museum-details/museum-details.component';
import { MuseumsComponent } from './components/museums/museums.component';

const routes: Routes = [
    {
      path: 'create',
      component: AddMuseumComponent,
    },
    {
      path: 'update/:id',
      component: AddMuseumComponent,
    },
    {
      path: 'search',
      component: MuseumsComponent,
    },
    {
      path: ':id',
      component: MuseumDetailsComponent,
    },
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  export class MuseumRoutingModule {}