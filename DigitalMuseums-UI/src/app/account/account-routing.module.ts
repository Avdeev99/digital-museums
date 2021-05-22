import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../core/auth/guards/auth.guard';
import { ExhibitModule } from '../exhibit/exhibit.module';
import { ExhibitionModule } from '../exhibition/exhibition.module';
import { MuseumModule } from '../museum/museum.module';
import { SouvenirModule } from '../souvenir/souvenir.module';
import { ChangePasswordComponent } from './components/change-password/change-password.component';
import { LoginComponent } from './components/login/login.component';
import { UserInfoEditingComponent } from './components/user-info-editing/user-info-editing.component';
import { UserMuseumComponent } from './components/user-museum/user-museum.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'change-password',
    component: ChangePasswordComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'user-info',
    component: UserInfoEditingComponent,
    canActivate: [AuthGuard],
  },
  {
    path: '',
    component: UserMuseumComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'museum',
        loadChildren: (): Promise<MuseumModule> => import('../museum/museum.module').then((m): MuseumModule => m.MuseumModule),
      },
      {
        path: 'exhibit',
        loadChildren: (): Promise<ExhibitModule> => import('../exhibit/exhibit.module').then((m): ExhibitModule => m.ExhibitModule),
      },
      {
        path: 'exhibition',
        loadChildren: (): Promise<ExhibitionModule> => import('../exhibition/exhibition.module').then((m): ExhibitionModule => m.ExhibitionModule),
      },
      {
        path: 'souvenir',
        loadChildren: (): Promise<SouvenirModule> => import('../souvenir/souvenir.module').then((m): SouvenirModule => m.SouvenirModule),
      }
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AccountRoutingModule {}
