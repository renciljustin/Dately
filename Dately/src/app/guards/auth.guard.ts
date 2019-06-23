import { AuthService } from 'src/app/services/auth.service';
import { CanActivate, RouterStateSnapshot, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Injectable } from '@angular/core';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(protected authService: AuthService, protected router: Router) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {

        if (this.authService.isAuthenticated()) {
            return true;
        }

        this.router.navigate(['/login'], {
            queryParams: {
                returnUrl: state.url
            }
        });

        return false;
    }
}
