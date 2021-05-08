import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { CurrentUserService } from '../../shared/services/current-user.service';
import { AuthService } from '../auth.service';
import { AuthRole } from '../models/auth-role.enum';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  public constructor(
    private router: Router,
    private authService: AuthService,
    private currentUserService: CurrentUserService) {}

  public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const isAuthenticated: boolean = this.authService.isAuthenticated();
    const currentUserRole: AuthRole = this.currentUserService.getRole();
    const requiredRoles: AuthRole[] = route.data.roles;

    const needToCheckRole: boolean = !!requiredRoles && !!requiredRoles.length;
    const isValidRole: boolean =
      !needToCheckRole || (!!requiredRoles && requiredRoles.includes(currentUserRole) && needToCheckRole);

    if (isAuthenticated && isValidRole) {
      return true;
    }

    this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });

    return false;
  }
}
