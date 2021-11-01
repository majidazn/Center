import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { JwtHelperService } from '@auth0/angular-jwt';
import { catchError, map } from "rxjs/operators";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { throwError as observableThrowError, Observable } from 'rxjs';
import { EnvService } from "../../../../env/env.service";
import { CenterApiService } from "../center-api.service";
import { SharedService } from "../../../../services/shared.service";




@Injectable()
export class CenterAuthGuard implements CanActivate {
  token: any;

  constructor(private _router: Router,
    private centerApiService: CenterApiService,
    private http: HttpClient, private env: EnvService,
    private _sharedService: SharedService) {

  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
     
    const helper = new JwtHelperService();
    this.token = this.getCookie("token");
   
    if (this.token == null || this.token == undefined || this.token == "undefined") {
      window.location.href = this.env.DefaultPortalAddress;
    }

    if (this.token !== null && this.token != undefined && this.token != "undefined") {

      const isExpired = helper.isTokenExpired(this.token);
      if (isExpired) {
        window.location.href = this.env.DefaultPortalAddress;
        return false;
      }
      if (this._sharedService.getUserTenantId() != 1) {
        window.location.href = this.env.DefaultPortalAddress;
        return false;
      }
      // logged in so return true
      return true;
    }

    window.location.href = this.env.DefaultPortalAddress;
    return false;
  }
  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
    return this.canActivate(route, state);
  }

  getCookie(name){
    var value = "; " + document.cookie;
    var parts = value.split("; " + name + "=");
    if (parts.length == 2) return parts.pop().split(";").shift();
  }
  public handleError(error: HttpErrorResponse): Observable<any> {
    return observableThrowError(error);
  }
}
