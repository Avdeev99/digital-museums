import { AddMuseumComponent } from './components/add-museum/add-museum.component';
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

const routes: Routes = [
    {
      path: '',
      component: AddMuseumComponent,
    },
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  export class MuseumRoutingModule {}