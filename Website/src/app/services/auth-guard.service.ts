import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router} from '@angular/router';
import { MsAdalAngular6Service } from 'microsoft-adal-angular6';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(
        private adalSvc: MsAdalAngular6Service,
        private router: Router
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        const { roles: requiredRoles } = route.data;
        const { isAuthenticated, userInfo: { profile: { groups: userRoles = [] } = { } } = { } } = this.adalSvc || {};

        if (!this.adalSvc.isAuthenticated) {
            return false;
        }

        const userHasRole = (profileRoles: string[], routeRoles: string[]) => {
            const upperProfileRoles = [].concat.apply([], profileRoles || []).filter(x => !!x).map(x => x.toUpperCase());
            const upperRouteRoles = [].concat.apply([], routeRoles || []).filter(x => !!x).map(x => x.toUpperCase());

            const hasRole = profileRoles && routeRoles
                && (upperRouteRoles.filter(r => upperProfileRoles.indexOf(r) >= 0).length > 0);

            return hasRole;
        };

        if (!userHasRole(userRoles, requiredRoles)) {
            this.router.navigate(['/forbidden']);
        }

        return true;
    }
}
