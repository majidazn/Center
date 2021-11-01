import { Injectable } from '@angular/core';
import { GridSettings } from '../models/grid-settings.interface';
import { MatDatepickerInputEvent } from "@angular/material";
import * as moment from "jalali-moment";
import { FormGroup } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { getToken } from '@angular/router/src/utils/preactivation';

declare var jquery: any;
declare var $: any;

@Injectable()
export class TokenInfoService {
  public token: any;

  constructor() {

  }

  getCurrentUser() {
    const helper = new JwtHelperService();
    this.token = this.getCookie("token");
    const decodedToken = helper.decodeToken(this.token);
    return decodedToken;

  }



  getCookie(name: any) {
    var value = "; " + document.cookie;
    var parts = value.split("; " + name + "=");
    if (parts.length == 2) return parts.pop().split(";").shift();
  }

  getToken() {
    this.token = this.getCookie("token");
    return this.token
  }

  clearToken() {
    document.cookie = 'token=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
    localStorage.removeItem('token');
    sessionStorage.removeItem('token');
  }

  goToLoginPage() {
    var baseURL = this.getToken()
    window.location.href = baseURL;
  }
}
