import { Injectable } from "@angular/core";
import { SafeUrl } from "@angular/platform-browser";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { JwtHelperService } from '@auth0/angular-jwt';  
import {Observable } from 'rxjs';
import { EnvService } from "../env/env.service";
import { SharedService } from "./shared.service";


@Injectable()
export class AuthGuard implements CanActivate {
  token: any = this.getCookie('token');

  constructor(private _env: EnvService, private _sharedService:SharedService, private router: Router) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
    debugger
    const helper = new JwtHelperService();
    if (this.token == null || this.token === undefined || this.token === 'undefined') {
      this.router.navigate(['/401']);
    }
    else if (this.token !== null && this.token !== undefined && this.token !== 'undefined') {
      const isExpired = helper.isTokenExpired(this.token);
      if (isExpired) {
        this.router.navigate(['/401']);
        return false;
      }
      // logged in so return true
      return true;
    }
    // window.location.href = this._env.HIS;
    return false;
  }

  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
    return this.canActivate(route, state);
  }

  getCookie(name) {
    const value = '; ' + document.cookie;
    const parts = value.split('; ' + name + '=');
    if (parts.length === 2) { return parts.pop().split(';').shift(); }
  }

  getUserRoleId() {
    const helper = new JwtHelperService();
    const decodedToken = helper.decodeToken(this.token);
    if (decodedToken) {
      if (decodedToken.RoleId.length === undefined) {
        const tokenId = [];
        tokenId.push(decodedToken.RoleId);
        return tokenId;
      }
      return decodedToken.RoleId;
    }
    else {
      return null;
    }
  }

  getUserRoleNames(): string[] {
    const helper = new JwtHelperService();
    const decodedToken = helper.decodeToken(this.token);
    if (decodedToken) {
      if (typeof decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] === 'string') {
        const roleNames = [];
        roleNames.push(decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);
        return roleNames;
      }
      return decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    }
  }

  getUserTenantId() {
    const helper = new JwtHelperService();
    const decodedToken = helper.decodeToken(this.token);
    if (decodedToken) {
      if (decodedToken.TenantId) {
        return +decodedToken.TenantId;
      }
      return null;
    }
  }
}
