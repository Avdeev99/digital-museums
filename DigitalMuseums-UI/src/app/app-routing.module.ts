import { MuseumModule } from './museum/museum.module';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AccountModule } from './account/account.module';
import { ExhibitModule } from './exhibit/exhibit.module';
import { SouvenirModule } from './souvenir/souvenir.module';
import { HomeComponent } from './layout/home/home.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'login',
    loadChildren: (): Promise<AccountModule> => import('./account/account.module').then((m): AccountModule => m.AccountModule),
  },
  {
    path: 'museum',
    loadChildren: (): Promise<MuseumModule> => import('./museum/museum.module').then((m): MuseumModule => m.MuseumModule),
  },
  {
    path: 'exhibit',
    loadChildren: (): Promise<ExhibitModule> => import('./exhibit/exhibit.module').then((m): ExhibitModule => m.ExhibitModule),
  },
  {
    path: 'souvenir',
    loadChildren: (): Promise<SouvenirModule> => import('./souvenir/souvenir.module').then((m): SouvenirModule => m.SouvenirModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
