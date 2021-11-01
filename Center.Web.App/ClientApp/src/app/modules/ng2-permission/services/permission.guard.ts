import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { PermissionService } from './permission.service';
import { DefinePermissionsAndRolesService } from '../../../services/define-permissions-and-roles.service';
import { IPermissionGuardModel } from '../model/permission-guard.model';
import { EnvService } from '../../../env/env.service';
import { SharedService } from '../../../services/shared.service';
import { Observable } from 'rxjs';

@Injectable()
export class PermissionGuard implements CanActivate {

  constructor(private _permissionService: PermissionService, private router: Router,
              private _sharedService: SharedService, private _env: EnvService,
              private _definePermissions: DefinePermissionsAndRolesService) {
    }

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    await this._definePermissions.init(this._permissionService);
        const token = this._sharedService.getCookie('token');

        if (token == null || token === undefined || token === 'undefined' || token === '') {
            this.router.navigate(['403']);
        }

        const data = route.data.Permission as IPermissionGuardModel;
        if (Array.isArray(data.Only) && Array.isArray(data.Except)) {
            throw new Error('can\'t use both \'Only\' and \'Except\' in route data.');
        }

        if (Array.isArray(data.Only)) {
            const hasDefined = this._permissionService.hasOneDefined(data.Only);
            if (hasDefined) {
                return true;
            }

            if (data.RedirectTo && data.RedirectTo !== undefined && this._sharedService.token) {
                this.router.navigate([data.RedirectTo]);
            }
            else {
              window.location.href = this._env.DefaultPortalAddress;
            }
            return false;
        }

        if (Array.isArray(data.Except)) {
            const hasDefined = this._permissionService.hasOneDefined(data.Except);
            if (!hasDefined) {
                return true;
            }

            if (data.RedirectTo && data.RedirectTo !== undefined) {
                this.router.navigate([data.RedirectTo]);
            }
            return false;
        }
   }
  async canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.canActivate(route, state);
  }
}
