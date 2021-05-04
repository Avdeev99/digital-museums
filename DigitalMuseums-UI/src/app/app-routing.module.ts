import { MuseumModule } from './museum/museum.module';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AccountModule } from './account/account.module';
import { ExhibitModule } from './exhibit/exhibit.module';
import { SouvenirModule } from './souvenir/souvenir.module';
import { ExhibitionModule } from './exhibition/exhibition.module';
import { CartModule } from './cart/cart.module';

const routes: Routes = [
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
  {
    path: 'exhibition',
    loadChildren: (): Promise<ExhibitionModule> => import('./exhibition/exhibition.module').then((m): ExhibitionModule => m.ExhibitionModule),
  },
  {
    path: 'cart',
    loadChildren: (): Promise<CartModule> => import('./cart/cart.module').then((m): CartModule => m.CartModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
