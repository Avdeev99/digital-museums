import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AccountModule } from './account/account.module';

const routes: Routes = [
  {
    path: 'login',
    loadChildren: (): Promise<AccountModule> => import('./account/account.module').then((m): AccountModule => m.AccountModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
